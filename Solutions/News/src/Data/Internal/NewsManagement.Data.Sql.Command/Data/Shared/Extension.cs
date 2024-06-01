namespace NewsManagement.Data.Sql.Commands;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public static partial class Extension
{
    public static DbContextOptionsBuilder SetDatabaseOptions(this DbContextOptionsBuilder source)
    => source
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging();
}