namespace Cloudio.Core.Security;

using System.Security.Cryptography;

public class HashVerification
{
    public static bool Verify(byte[] input1, byte[] input2)
    {
        var result = CryptographicOperations.FixedTimeEquals(input1, input2);
        return result;
    }
}