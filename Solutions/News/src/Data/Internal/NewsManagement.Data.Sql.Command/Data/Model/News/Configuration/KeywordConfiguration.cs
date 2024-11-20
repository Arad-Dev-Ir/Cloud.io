namespace NewsManagement.Data.Sql.News.Commands;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloudio.Web.Data.Sql.Command;
using NewsManagement.Core.News.Models;
using NewsManagement.Data.Sql.Commands;

public sealed class KeywordConfiguration : Configuration<Keyword>
{
    public override void Configure(EntityTypeBuilder<Keyword> builder)
    => Initialize(builder);

    private static void Initialize(EntityTypeBuilder<Keyword> builder)
    => builder.ToTable(NewsManagementCommandDbContextSchema.NewsKeywordsSchema.TableName);
}