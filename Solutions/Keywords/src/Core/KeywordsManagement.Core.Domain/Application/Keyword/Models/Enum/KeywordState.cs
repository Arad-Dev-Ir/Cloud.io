namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core;
using Cloud.Core.Models;

public sealed record KeywordState : Enumer
{
    public static KeywordState Preview { get; private set; } = new(nameof(Preview));
    public static KeywordState Active { get; private set; } = new(nameof(Active));
    public static KeywordState Inactive { get; private set; } = new(nameof(Inactive));


    public static List<KeywordState> Items { get; private set; } = [];

    public override string Display
    => Value switch
    {
        "Preview" => "پیش نمایش",
        "Active" => "فعال",
        "Inactive" => "غیر فعال",
        _ => "پیش نمایش"
    };

    #region Initialize

    public KeywordState() : base(Atom.Empty)
    { }
    public KeywordState(string value) : base(value.IsEmpty() ? Atom.Empty : value)
    { }
    static KeywordState()
    => Initialize();

    static void Initialize()
    {
        Items.Add(new KeywordState());
        Items.Add(new(nameof(Preview)));
        Items.Add(new(nameof(Active)));
        Items.Add(new(nameof(Inactive)));
    }

    #endregion
}