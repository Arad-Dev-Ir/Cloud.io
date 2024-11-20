namespace Cloudio.Core.Security;

public static class BitConversion
{
    public static string Encode(byte[] input)
    {
        var result = BitConverter.ToString(input).Replace("-", String.Empty).ToLowerInvariant();
        return result;
    }
}