namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloudio.Web.Data.Sql.Command;
using KeywordsManagement.Core.Keyword.Models;
using KeywordsManagement.Data.Sql.Commands;

internal sealed class KeywordConfiguration : Configuration<Keyword>
{
    public override void Configure(EntityTypeBuilder<Keyword> entityType)
    => Initialize(entityType);

    private static void Initialize(EntityTypeBuilder<Keyword> entityType)
    {
        entityType.ToTable(KeywordsManagementCommandDbContextSchema.KeywordSchema.TableName);

        entityType
        .HasIndex(e => e.Title)
        .IsUnique();

        entityType.Property(e => e.Title)
        .HasMaxLength(50)
        .IsRequired()
        .HasConversion<KeywordTitleConversion>();

        entityType.Property(e => e.State)
        .HasMaxLength(20)
        .IsRequired()
        .IsUnicode(false)
        .HasConversion<KeywordStateConversion>();
    }
}
