namespace KeywordsManagement.Core.Keyword.Models;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed record KeywordState : Enumer
{
    public static KeywordState Preview { get; private set; } = new(nameof(Preview));
    public static KeywordState Active { get; private set; } = new(nameof(Active));
    public static KeywordState Inactive { get; private set; } = new(nameof(Inactive));

    private static List<KeywordState> _items = [];
    public static IReadOnlyList<KeywordState> Items => _items;

    public override string Display
    => Value switch
    {
        "Preview" => "پیش نمایش",
        "Active" => "فعال",
        "Inactive" => "غیر فعال",
        _ => "پیش نمایش"
    };

    #region Initialize

    private KeywordState() : base(Atom.Empty)
    { }
    private KeywordState(string value) : base(value.IsEmpty() ? Atom.Empty : value)
    { }
    static KeywordState()
    => Initialize();

    static void Initialize()
    {
        _items.AddRange(
        [
            new KeywordState(),
            new(nameof(Preview)),
            new(nameof(Active)),
            new(nameof(Inactive)),
        ]);
    }

    public static KeywordState CreateInstance(string state)
    {
        if (_items.Contains(new(state)))
            return new KeywordState(state);
        else
            throw new Exception($"{state} is invalid");
        throw new ElementInvalidException(nameof(KeywordState), state);
    }

    #endregion
}