namespace Cloudio.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class OutboxEventConfiguration : Configuration<OutboxEvent>
{
    public override void Configure(EntityTypeBuilder<OutboxEvent> builder)
    => Initialize(builder);

    private static void Initialize(EntityTypeBuilder<OutboxEvent> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(255);
        builder.Property(e => e.UserId).HasMaxLength(255);
        builder.Property(e => e.Type).HasMaxLength(255);

        builder.Property(e => e.State)
        .HasMaxLength(20)
        .IsUnicode(false)
        .HasConversion<ProcessModeConversion>();

        builder.Property(e => e.ModuleName).HasMaxLength(255);
        builder.Property(e => e.ModuleCode).HasMaxLength(255);
        builder.Property(e => e.ModuleType).HasMaxLength(500);
    }
}
