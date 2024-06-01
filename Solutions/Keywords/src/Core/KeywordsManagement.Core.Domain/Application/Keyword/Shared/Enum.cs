namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core;
using Cloud.Core.Models;

public class Mode : Enumer
{
    public static Mode Preview { get; private set; } = new(nameof(Preview));
    public static Mode Active { get; private set; } = new(nameof(Active));
    public static Mode Inactive { get; private set; } = new(nameof(Inactive));


    public static List<Mode> Items { get; private set; } = [];

    public override string Display
    => Value switch
    {
        "Preview" => "پیش نمایش",
        "Active" => "فعال",
        "Inactive" => "غیر فعال",
        _ => "پیش نمایش"
    };

    #region Initialize

    public Mode() : base(Empty)
    { }
    public Mode(string value) : base(value.IsEmpty() ? Empty : value)
    { }
    static Mode()
    => Initialize();

    static void Initialize()
    {
        Items.Add(new Mode());
        Items.Add(new(nameof(Preview)));
        Items.Add(new(nameof(Active)));
        Items.Add(new(nameof(Inactive)));
    }

    #endregion
}