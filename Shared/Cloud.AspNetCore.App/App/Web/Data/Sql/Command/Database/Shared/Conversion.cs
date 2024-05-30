namespace Cloud.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

public interface IConversion
{ }

public abstract class Conversion<M, P> : ValueConverter<M, P>, IConversion
{
    public Conversion(Expression<Func<M, P>> convert, Expression<Func<P, M>> convertBack) : base(convert, convertBack)
    { }
}