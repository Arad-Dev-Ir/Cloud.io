namespace Cloud.Core;

using System.Linq.Expressions;
using Encoding = System.Text.Encoding;

public static partial class Extension
{
    public static bool IsEmpty(this string source)
    => String.IsNullOrWhiteSpace(source);

    public static bool IsNotEmpty(this string source)
    => !source.IsEmpty();

    public static void IsNotEmpty(this string source, Action act)
    {
        if (source.IsNotEmpty()) act.Invoke();
    }

    public static bool IsLengthBetween(this string source, int minChar, int maxChar)
    => source.Length >= minChar && source.Length <= maxChar;

    public static bool Is<T>(this object source)
    => source is T;

    public static void IsNull<T>(this T source, Action act)
    => source.IsNull(act, () => { });

    public static void IsNull<T>(this T source, Action @if, Action @else)
    {
        if (source is null) @if.Invoke();
        else @else.Invoke();
    }

    public static async Task IsNull<T>(this T source, Func<Task> act)
    {
        if (source is null) await act.Invoke();
    }

    public static async Task IsNull<T>(this T source, Func<Task> @if, Func<Task> @else)
    {
        if (source is null) await @if.Invoke();
        else await @else.Invoke();
    }

    public static void IsNotNull<T>(this T source, Action act)
    => source.IsNotNull(act, () => { });

    public static void IsNotNull<T>(this T source, Action @if, Action @else)
    {
        if (source is not null) @if.Invoke();
        else @else.Invoke();
    }

    public static async Task IsNotNull<T>(this T source, Func<Task> act)
    {
        if (source is not null) await act.Invoke();
    }

    public static T As<T>(this object source)
    => (T)source;
}

// primitive
public static partial class Extension
{
    public static T To<T>(this object source)
    {
        var result = default(T);
        var typeCode = Type.GetTypeCode(typeof(T));
        result = (T)Convert.ChangeType(source, typeCode);
        return result;
    }

    public static int ToInt(this object source)
    => source.To<int>();

    public static double ToDouble(this object source)
    => source.To<double>();

    public static bool ToBoolean(this object source)
    => source.To<bool>();
}

public static partial class Extension
{
    public static byte[] UTFGet32Bytes(this string source)
    => Encoding.UTF32.GetBytes(source);

    public static string UTF32GetString(this byte[] source)
    => Encoding.UTF32.GetString(source);

    public static byte[] UTF8GetBytes(this string source)
    => Encoding.UTF8.GetBytes(source);

    public static string UTF8GetString(this byte[] source)
    => Encoding.UTF8.GetString(source);
}

// linq
public static partial class Extension
{
    public static IQueryable<T> Where<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
    => condition ? source.Where(predicate) : source;

    public static IQueryable<T> Where<T>(this IQueryable<T> source, bool condition, Expression<Func<T, int, bool>> predicate)
    => condition ? source.Where(predicate) : source;

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