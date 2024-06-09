namespace NewsManagement.Data.Sql.News.Commands;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloud.Web.Data.Sql.Command;
using Core.News.Models;

public sealed class NewsConfiguration : Configuration<News>
{
    public override void Configure(EntityTypeBuilder<News> builder)
    => Initialize(builder);

    private static void Initialize(EntityTypeBuilder<News> builder)
    {
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