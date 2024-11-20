namespace Cloudio.Core.Security;

using System.Security.Cryptography;

public class SHA256Hash : IHash
{
    public string Compute(byte[] input)
    {
        var hash = SHA256.HashData(input);

        var result = BitConversion.Encode(hash);
        return result;
    }

    public string Compute(string input)
    {
        var bytes = input.Bytes();

        var result = Compute(bytes);
        return result;
    }
}
