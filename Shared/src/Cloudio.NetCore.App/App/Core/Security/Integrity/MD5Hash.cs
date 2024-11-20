namespace Cloudio.Core.Security;

using System.Security.Cryptography;

public class MD5Hash : IHash
{
    public string Compute(byte[] input)
    {
        var hash = MD5.HashData(input);

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