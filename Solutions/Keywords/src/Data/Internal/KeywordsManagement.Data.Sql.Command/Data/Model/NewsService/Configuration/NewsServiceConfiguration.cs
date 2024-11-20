namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloudio.Web.Data.Sql.Command;
using KeywordsManagement.Core.NewsService.Models;
using KeywordsManagement.Data.Sql.Commands;

internal sealed class NewsServiceConfiguration : Configuration<NewsService>
{
    public override void Configure(EntityTypeBuilder<NewsService> entityType)
    => Initialize(entityType);

    private static void Initialize(EntityTypeBuilder<NewsService> entityType)
    {
        entityType.ToTable(KeywordsManagementCommandDbContextSchema.NewsServicesSchema.TableName);

        entityType
        .HasIndex(e => e.Name)
        .IsUnique();

        entityType.Property(e => e.Name)
        .HasMaxLength(50)
        .IsRequired()
        .HasConversion<NewsServiceNameConversion>();

        entityType.Property(e => e.Title)
        .HasMaxLength(50)
        .IsRequired()
        .HasConversion<NewsServiceTitleConversion>();
    }
}
