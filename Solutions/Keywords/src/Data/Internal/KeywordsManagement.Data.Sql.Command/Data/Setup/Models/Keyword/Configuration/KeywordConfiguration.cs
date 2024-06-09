namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloud.Web.Data.Sql.Command;
using Core.Keyword.Models;

internal sealed class KeywordConfiguration : Configuration<Keyword>
{
    public override void Configure(EntityTypeBuilder<Keyword> entityType)
    => Initialize(entityType);

    private static void Initialize(EntityTypeBuilder<Keyword> entityType)
    {
        entityType.Property(e => e.Title)
        .HasMaxLength(50)
        .HasConversion<KeywordTitleConversion>();

        entityType.Property(e => e.State)
        .HasMaxLength(20)
        .HasConversion<KeywordStateConversion>();
    }
}
