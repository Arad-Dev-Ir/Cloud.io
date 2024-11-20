namespace Cloudio.Web.Data.Sql.Command;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed record EventProcessingState : Enumer
{
    public static EventProcessingState Raised { get; private set; } = new(nameof(Raised));
    public static EventProcessingState Registered { get; private set; } = new(nameof(Registered));
    public static EventProcessingState Processed { get; private set; } = new(nameof(Processed));

    private static readonly List<EventProcessingState> _items = [];
    public static IReadOnlyCollection<EventProcessingState> Items => [.. _items];

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

    private EventProcessingState() : base(Atom.Empty)
    { }
    private EventProcessingState(string value) : base(value.IsEmpty() ? Atom.Empty : value)
    { }
    static EventProcessingState()
    => Initialize();

    static void Initialize()
    {
        _items.AddRange(
        [
            new EventProcessingState(),
            new(nameof(Raised)),
            new(nameof(Registered)),
            new(nameof(Processed)),
        ]);
    }

    public static EventProcessingState Instance(string mode)
    {
        if (_items.Contains(new(mode)))
            return new EventProcessingState(mode);
        else
            throw new Exception($"{mode} is invalid!");
    }

    #endregion
}