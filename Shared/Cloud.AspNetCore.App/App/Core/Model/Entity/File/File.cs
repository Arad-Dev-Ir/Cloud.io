namespace Cloud.Core.Models;

internal class File : Entity
{
    private File()
    { }

    public string Name { get; set; } = Empty;

    public string Mime { get; set; } = Empty;

    public Bytes Bytes { get; set; }
}

public record Bytes : Element
{

    public string Alias { get; set; }

    public byte[] Source { get; set; }
}