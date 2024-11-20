namespace Cloudio.Core.Security;

using System.Security.Cryptography;

public static class Token
{
    /// <summary>
    /// Creates a Random Bytes by RandomNumberGenerator class
    /// </summary>
    /// <param name="digit">digit of bytes of needed token</param>
    /// <returns></returns>
    public static byte[] Create(int digit)
    {
        var result = new byte[digit];
        using var randomer = RandomNumberGenerator.Create();
        randomer.GetBytes(result);

        return result;
    }
}