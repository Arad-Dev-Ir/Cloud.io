namespace NewsManagement.Data.External.Messaging;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Cloudio.Web.Core.Contract;

public class KeywordEventHandler : IEventHandler<Keyword>
{
    private readonly SqlConnection _connection;

    public KeywordEventHandler(IOptionsMonitor<DatabaseConfigs> options)
    {
        var connectionString = options.CurrentValue.ConnectionString;
        _connection = new(connectionString);
    }

    public async Task HandleAsync(Keyword @event, CancellationToken token)
    {
        await OpenConnection();

        var exists = await ExistsAsync(@event.Code);
        if (exists)
        {
            await UpdateAsync(@event);
        }
        else
            await SaveAsync(@event);

        await CloseConnection();
    }

    #region Database Commands

    private async Task SaveAsync(Keyword entity)
    {
        var insertCommand = $"insert into [dbo].[Keywords] ([Code], [Title]) VALUES (@Code, @Title)";
        var command = new SqlCommand(insertCommand, _connection);

        SqlParameter[] parameters =
        [
            new("Code", entity.Code),
            new("Title", entity.Title),
        ];
        command.Parameters.AddRange(parameters);
        await command.ExecuteNonQueryAsync();
        _connection.Close();
    }

    private async Task<bool> ExistsAsync(Guid code)
    {
        bool result = default;

        var countCommand = "select count(*) from [dbo].[Keywords] where [Code] = @Code";
        var command = new SqlCommand(countCommand, _connection);
        SqlParameter[] parameters = [new("Code", code)];
        command.Parameters.AddRange(parameters);

        var count = (int?)await command.ExecuteScalarAsync();
        if (count is not null && count > 0)
            result = true;

        return result;
    }

    private async Task UpdateAsync(Keyword entity)
    {
        var selectCommand = "update [dbo].[Keywords] set [Title] = @Title where [Code] = @Code";
        var command = new SqlCommand(selectCommand, _connection);
        SqlParameter[] parameters =
        [
            new("Code", entity.Code),
            new("Title", entity.Title),
        ];
        command.Parameters.AddRange(parameters);
        await command.ExecuteNonQueryAsync();
    }

    private async Task OpenConnection()
        => await _connection.OpenAsync();

    private async Task CloseConnection()
        => await _connection?.CloseAsync()!;

    #endregion
}