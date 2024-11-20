namespace Cloudio.Core.Security;

public static class Base64
{
    public static string Encode(byte[] input)
    {
        var result = Convert.ToBase64String(input);
        return result;
    }

    public static byte[] Decode(string input)
    {
        var result = Convert.FromBase64String(input);
        return result;
    }
}