namespace Cloudio.Core;

using System.Linq.Expressions;
using Encoding = System.Text.Encoding;

public static partial class Extensions
{
    /// <summary>
    /// NullOrWhiteSpace
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsEmpty(this string source)
    {
        var result = string.IsNullOrWhiteSpace(source);
        return result;
    }

    /// <summary>
    /// NullOrWhiteSpace
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsNotEmpty(this string source)
    {
        var result = !source.IsEmpty();
        return result;
    }

    /// <summary>
    /// checks equality with comparison type 'InvariantCultureIgnoreCase'
    /// </summary>
    public static bool Is(this string source, string other)
    {
        var result = string.Equals(source, other, StringComparison.InvariantCultureIgnoreCase);
        return result;
    }

    /// <summary>
    /// checks equality with comparison type 'InvariantCultureIgnoreCase'
    /// </summary>
    public static bool IsNot(this string source, string other)
    {
        var result = !source.Is(other);
        return result;
    }

    public static string FormatByArguments(this string source, params object?[] args)
    {
        var result = string.Format(source, args);
        return result;
    }

    public static bool IsLengthBetween(this string source, int minChars, int maxChars)
    {
        var result = source.Length >= minChars && source.Length <= maxChars;
        return result;
    }

    public static bool IsLengthGreaterThanOrEqual(this string source, int minChars)
    {
        var result = source.Length >= minChars;
        return result;
    }

    public static bool IsLengthLessThanOrEqual(this string source, int maxChars)
    {
        var result = source.Length <= maxChars;
        return result;
    }

    public static bool Is<T>(this object source)
    {
        var result = source is T;
        return result;
    }

    public static T As<T>(this object source)
    {
        var result = (T)source;
        return result;
    }

    public static T To<T>(this object source)
    {
        var typeCode = Type.GetTypeCode(typeof(T));

        var result = (T)Convert.ChangeType(source, typeCode);
        return result;
    }

    public static int ToInt(this object source)
    {
        var result = source.To<int>();
        return result;
    }

    public static double ToDouble(this object source)
    {
        var result = source.To<double>();
        return result;
    }

    public static bool ToBoolean(this object source)
    {
        var result = source.To<bool>();
        return result;
    }

    /// <summary>
    /// returns Encoding.UTF32.GetBytes
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static byte[] Bytes32(this string source)
    {
        var result = Encoding.UTF32.GetBytes(source);
        return result;
    }

    /// <summary>
    /// returns Encoding.UTF32.GetString
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string String32(this byte[] source)
    {
        var result = Encoding.UTF32.GetString(source);
        return result;
    }

    /// <summary>
    /// returns Encoding.UTF8.GetBytes
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static byte[] Bytes(this string source)
    {
        var result = Encoding.UTF8.GetBytes(source);
        return result;
    }

    /// <summary>
    /// returns Encoding.UTF8.GetString
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string String(this byte[] source)
    {
        var result = Encoding.UTF8.GetString(source);
        return result;
    }

    public static IQueryable<T> Where<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
    {
        var result = condition ? source.Where(predicate) : source;
        return result;
    }

    public static IQueryable<T> Where<T>(this IQueryable<T> source, bool condition, Expression<Func<T, int, bool>> predicate)
    {
        var result = condition ? source.Where(predicate) : source;
        return result;
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string property, bool ascending)
    {
        var parameter = Expression.Parameter(typeof(T), "e");
        var body = Expression.Property(parameter, property);
        var lambdaExpression = Expression.Lambda(body, parameter);
        var types = new Type[] { source.ElementType, lambdaExpression.Body.Type };
        var method = ascending ? "OrderBy" : "OrderByDescending";
        var mce = Expression.Call(typeof(Queryable), method, types, source.Expression, lambdaExpression);
        var result = source.Provider.CreateQuery<T>(mce);

        return result;
    }
}