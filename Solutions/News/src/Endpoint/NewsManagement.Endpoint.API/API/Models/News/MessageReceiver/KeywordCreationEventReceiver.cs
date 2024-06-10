namespace NewsManagement.Endpoint.News.APIs;

using Microsoft.Data.SqlClient;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Cloud.Core;
using Cloud.Core.Models;
using Cloud.Core.Extensions.Serialization;

public sealed class KeywordCreationEventReceiver : BackgroundService
{
    private readonly IJsonSerializer _serializer;
    private readonly SqlConnection _sqlConnection;

    public KeywordCreationEventReceiver(IJsonSerializer serializer)
    {
        _serializer = serializer;

        var host = "localhost";
        var connection = new ConnectionFactory { HostName = host }.CreateConnection();
        var model = connection.CreateModel();

        var queue = model.QueueDeclare().QueueName;

        var exchangeName = "Cloudio";
        model.ExchangeDeclare(exchangeName, ExchangeType.Topic, false, true, null);

        var key = $"{exchangeName}.KeywordsManagement.Event.KeywordCreated";
        model.QueueBind(queue, exchangeName, key, null);

        _sqlConnection = new SqlConnection("Server =.\\DEVELOP; Initial Catalog = NewsManagement_Db; Persist Security Info = True; User Id = sa; Password = 13680805; Trust Server Certificate = true; MultipleActiveResultSets = true; Encrypt = false");
        _sqlConnection.Open();
        var consumer = new EventingBasicConsumer(model);
        consumer.Received += (o, e) =>
        {
            var data = _serializer.Deserialize<Keyword>(e.Body.ToArray().UTF8GetString());
            var command = $"INSERT INTO [dbo].[Keywords] ([Code], [Title]) VALUES ('{data.Code}','{data.Title}')";
            var sqlCommand = new SqlCommand(command, _sqlConnection);
            sqlCommand.ExecuteNonQuery();
        };
        model.BasicConsume(queue, true, consumer);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
            await Task.Delay(2000, stoppingToken);
    }
}

public class Keyword : Model
{
    public Guid Code { get; set; }
    public string Title { get; set; } = Empty;
}