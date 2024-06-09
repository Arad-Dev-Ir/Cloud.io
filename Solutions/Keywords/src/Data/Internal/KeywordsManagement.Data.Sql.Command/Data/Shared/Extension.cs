namespace KeywordsManagement.Data.Sql.Commands;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public static partial class Extension
{
    public static DbContextOptionsBuilder SetDatabaseLogOptions(this DbContextOptionsBuilder source)
    => source
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging();
}