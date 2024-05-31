namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloud.Web.Data.Sql.Command;
using Core.NewsService.Models;

internal class NewsServiceConfiguration : Configuration<NewsService>
{
    public override void Configure(EntityTypeBuilder<NewsService> entityType)
    => Initialize(entityType);

    private void Initialize(EntityTypeBuilder<NewsService> entityType)
    {
        entityType.Property(e => e.Title)
        .HasMaxLength(50)
        .HasConversion<TitleConversion>();

        entityType.Property(e => e.Name)
        .HasMaxLength(50)
        .HasConversion<NameConversion>();
    }
}
