namespace Cloudio.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cloudio.Core.Models;

public abstract class Configuration<T> : IEntityTypeConfiguration<T> where T : Model
{
    public abstract void Configure(EntityTypeBuilder<T> builder);
}