namespace NewsManagement.Data.Sql.News.Commands;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloud.Web.Data.Sql.Command;
using Keyword = Core.News.Models.Keyword;

public class KeywordConfiguration : Configuration<Keyword>
{
    public override void Configure(EntityTypeBuilder<Keyword> builder)
    => Initialize(builder);

    private void Initialize(EntityTypeBuilder<Keyword> builder)
    => builder.ToTable("NewsKeywords");
}