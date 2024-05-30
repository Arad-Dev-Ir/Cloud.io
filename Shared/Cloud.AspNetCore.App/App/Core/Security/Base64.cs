namespace Cloud.Core.Security;

public static class Base64
{
    public static string Encode(byte[] input)
    => Convert.ToBase64String(input);

    public static byte[] Decode(string input)
    => Convert.FromBase64String(input);
}