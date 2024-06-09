namespace KeywordsManagement.Data.Sql.Queries;

using Microsoft.EntityFrameworkCore;
using Cloud.Web.Data.Sql.Query;
using Keyword = Keyword.Queries.Keyword;

public class KeywordsManagementQueryContext(DbContextOptions<KeywordsManagementQueryContext> options) : QueryContext(options)
{
    public DbSet<Keyword> Keywords { get; set; }
}