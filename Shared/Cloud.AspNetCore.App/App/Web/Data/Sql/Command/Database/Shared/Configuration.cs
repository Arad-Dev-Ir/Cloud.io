namespace Cloud.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloud.Core.Models;

public abstract class Configuration<T> : IEntityTypeConfiguration<T> where T : Model
{
    public abstract void Configure(EntityTypeBuilder<T> builder);
}