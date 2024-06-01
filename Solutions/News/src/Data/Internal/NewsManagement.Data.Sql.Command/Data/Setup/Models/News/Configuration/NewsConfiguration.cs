namespace NewsManagement.Data.Sql.News.Commands;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloud.Web.Data.Sql.Command;
using News = Core.News.Models.News;

public class NewsConfiguration : Configuration<News>
{
    public override void Configure(EntityTypeBuilder<News> builder)
    => Initialize(builder);

    private void Initialize(EntityTypeBuilder<News> builder)
    {
        builder.Property(e => e.Title)
        .HasMaxLength(250)
        .HasConversion<TitleConversion>();

        builder.Property(e => e.Description)
        .HasMaxLength(500)
        .HasConversion<DescriptionConversion>();

        builder.Property(e => e.Body)
       .HasConversion<BodyConversion>();
    }
}