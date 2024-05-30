namespace Cloud.Core.Security;

public static class BitConversion
{
    public static string Encode(byte[] input)
    => BitConverter.ToString(input).Replace("-", String.Empty).ToLowerInvariant();
}