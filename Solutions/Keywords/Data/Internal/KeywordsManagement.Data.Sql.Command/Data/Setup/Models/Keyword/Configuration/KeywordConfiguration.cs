namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloud.Web.Data.Sql.Command;
using Core.Keyword.Models;

internal class KeywordConfiguration : Configuration<Keyword>
{
    public override void Configure(EntityTypeBuilder<Keyword> entityType)
    => Initialize(entityType);

    private void Initialize(EntityTypeBuilder<Keyword> entityType)
    {
        entityType.Property(e => e.Title)
        .HasMaxLength(50)
        .HasConversion<TitleConversion>();

        entityType.Property(e => e.Mode)
        .HasMaxLength(20)
        .HasConversion<ModeConversion>();
    }
}
