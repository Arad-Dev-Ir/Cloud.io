namespace Cloud.Web.Data.Sql.Command;

using Cloud.Core;
using Cloud.Core.Models;

public sealed record ProcessMode : Enumer
{
    public static ProcessMode Raised { get; private set; } = new(nameof(Raised));
    public static ProcessMode Registered { get; private set; } = new(nameof(Registered));
    public static ProcessMode Processed { get; private set; } = new(nameof(Processed));


    public static List<ProcessMode> Items { get; private set; } = new();

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

    public ProcessMode() : base(Atom.Empty)
    { }
    public ProcessMode(string value) : base(value.IsEmpty() ? Atom.Empty : value)
    { }
    static ProcessMode()
    => Initialize();

    static void Initialize()
    {
        Items.Add(new ProcessMode());
        Items.Add(new(nameof(Raised)));
        Items.Add(new(nameof(Registered)));
        Items.Add(new(nameof(Processed)));
    }

    #endregion
}