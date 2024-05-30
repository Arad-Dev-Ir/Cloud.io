namespace Cloud.Core.Security;

using System.Security.Cryptography;

public interface IHash
{
    string Compute(byte[] input);
    string Compute(string input);
}

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
        var bytes = input.UTF8GetBytes();
        var result = Compute(bytes);
        return result;
    }
}

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
        var bytes = input.UTF8GetBytes();
        var result = Compute(bytes);
        return result;
    }
}


public class HashVerification
{
    public static bool Verify(byte[] input1, byte[] input2)
    => CryptographicOperations.FixedTimeEquals(input1, input2);
}