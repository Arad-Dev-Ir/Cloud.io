namespace Cloud.Web.Data.Sql.Command;

using Cloud.Core;
using Cloud.Core.Models;

public sealed record ProcessMode : Enumer
{
    public static ProcessMode Raised { get; private set; } = new(nameof(Raised));
    public static ProcessMode Registered { get; private set; } = new(nameof(Registered));
    public static ProcessMode Processed { get; private set; } = new(nameof(Processed));

    private static readonly List<ProcessMode> _items = [];
    public static IReadOnlyList<ProcessMode> Items => _items;

    public override string Display
    {
        get
        {
            var result = Value switch
            {
                "Raised" => "منتشر شده",
                "Registered" => "ثبت شده",
                "Processed" => "پردازش شده",
                _ => "منتشر شده"
            };
            return result;
        }
    }

    #region Initialize

    private ProcessMode() : base(Atom.Empty)
    { }
    private ProcessMode(string value) : base(value.IsEmpty() ? Atom.Empty : value)
    { }
    static ProcessMode()
    => Initialize();

    static void Initialize()
    {
        _items.AddRange(
        [
            new ProcessMode(),
            new(nameof(Raised)),
            new(nameof(Registered)),
            new(nameof(Processed)),
        ]);
    }

    public static ProcessMode Instance(string mode)
    {
        if (_items.Contains(new(mode)))
            return new ProcessMode(mode);
        else
            throw new Exception($"{mode} is invalid!");
    }

    #endregion
}