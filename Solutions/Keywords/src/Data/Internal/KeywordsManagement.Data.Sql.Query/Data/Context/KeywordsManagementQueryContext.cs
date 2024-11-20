namespace KeywordsManagement.Data.Sql.Queries;

using Microsoft.EntityFrameworkCore;
using Cloudio.Web.Data.Sql.Query;
using KeywordsManagement.Data.Sql.Keyword.Queries;

public class KeywordsManagementQueryContext(DbContextOptions<KeywordsManagementQueryContext> options) : QueryContext(options)
{
    public DbSet<Keyword> Keywords { get; set; }
}