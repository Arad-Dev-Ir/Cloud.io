namespace KeywordsManagement.Data.Sql.Queries;

using Microsoft.EntityFrameworkCore;
using Cloud.Web.Data.Sql.Query;
using Keyword.Queries;

public class KeywordsManagementQueryContext(DbContextOptions<KeywordsManagementQueryContext> options) : QueryContext(options)
{
    public DbSet<Keyword> Keywords { get; set; }
}