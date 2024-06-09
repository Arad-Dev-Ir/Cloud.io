namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloud.Web.Data.Sql.Command;
using Core.NewsService.Models;

internal sealed class NewsServiceConfiguration : Configuration<NewsService>
{
    public override void Configure(EntityTypeBuilder<NewsService> entityType)
    => Initialize(entityType);

    private static void Initialize(EntityTypeBuilder<NewsService> entityType)
    {
        entityType.Property(e => e.Title)
        .HasMaxLength(50)
        .HasConversion<NewsServiceTitleConversion>();

        entityType.Property(e => e.Name)
        .HasMaxLength(50)
        .HasConversion<NewsServiceNameConversion>();
    }
}
