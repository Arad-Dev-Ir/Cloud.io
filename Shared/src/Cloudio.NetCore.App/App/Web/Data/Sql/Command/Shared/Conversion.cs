namespace Cloudio.Web.Data.Sql.Command;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public abstract class Conversion<M, P> : ValueConverter<M, P>, IConversion
{
    public Conversion(Expression<Func<M, P>> convert, Expression<Func<P, M>> convertBack) : base(convert, convertBack)
    { }
}