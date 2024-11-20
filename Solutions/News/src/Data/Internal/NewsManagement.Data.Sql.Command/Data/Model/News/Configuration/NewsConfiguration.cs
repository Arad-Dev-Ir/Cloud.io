namespace NewsManagement.Data.Sql.News.Commands;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloudio.Web.Data.Sql.Command;
using NewsManagement.Core.News.Models;
using NewsManagement.Data.Sql.Commands;

public sealed class NewsConfiguration : Configuration<News>
{
    public override void Configure(EntityTypeBuilder<News> builder)
    => Initialize(builder);

    private static void Initialize(EntityTypeBuilder<News> builder)
    {
        builder.ToTable(NewsManagementCommandDbContextSchema.NewsSchema.TableName);

        builder
        .HasIndex(e => e.Title)
        .IsUnique();

        builder.Property(e => e.Title)
        .HasMaxLength(250)
        .HasConversion<NewsTitleConversion>();

        builder.Property(e => e.Description)
        .HasMaxLength(500)
        .HasConversion<NewsDescriptionConversion>();

        builder.Property(e => e.Body)
       .HasConversion<NewsBodyConversion>();
    }
}