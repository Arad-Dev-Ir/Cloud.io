namespace Cloud.Core.Models;

internal class File : Entity
{
    private File()
    { }

    public string Name { get; set; } = Empty;

    public string Mime { get; set; } = Empty;

    public Bytes Bytes { get; set; }
}

public class Bytes : Element
{
    public Bytes()
    { }

    public string Alias { get; set; } = Empty;

    public byte[] Source { get; set; }

    protected override IEnumerable<object> Lookup()
    {
        throw new NotImplementedException();
    }
}