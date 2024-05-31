namespace KeywordsManagement.Data.Sql.Queries;

using Microsoft.EntityFrameworkCore;
using Cloud.Web.Data.Sql.Query;
using Keyword = Keyword.Queries.Keyword;

public class KeywordsManagementQueryContext : QueryContext
{
    public DbSet<Keyword> Keywords { get; set; }

    public KeywordsManagementQueryContext(DbContextOptions<KeywordsManagementQueryContext> options) : base(options)
    { }
}