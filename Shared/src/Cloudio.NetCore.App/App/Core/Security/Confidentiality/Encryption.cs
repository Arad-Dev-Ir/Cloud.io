namespace Cloudio.Core.Security;

using System.Security.Cryptography;

public class AESEncryption : IEncryption
{
    private const int KeySize = 256;
    private const int BlockSize = KeySize / 2;
    private const int IvSize = BlockSize / 8;

    private Aes GetAlgorithm()
    {
        var result = Aes.Create();

        result.KeySize = KeySize;
        result.BlockSize = BlockSize;
        result.Padding = PaddingMode.ISO10126;
        result.Mode = CipherMode.CBC;

        return result;
    }

    public byte[] Encrypt(byte[] input, string key)
    {
        var result = default(byte[]);

        var salt = Token.Create(IvSize); // 32B
        var iv = Token.Create(IvSize); // 32B
        var plainText = input;
        var keyBytes = Rfc2898DeriveBytes.Pbkdf2(key, salt, 1000, HashAlgorithmName.SHA256, IvSize); //32B

        using var algorithm = GetAlgorithm();
        using (var encryptor = algorithm.CreateEncryptor(keyBytes, iv))
        {
            using (var memory = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memory, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainText, 0, plainText.Length);
                    cryptoStream.FlushFinalBlock();
                    var bytes = salt;
                    bytes = bytes.Concat([.. iv]).ToArray();
                    bytes = bytes.Concat(memory.ToArray()).ToArray();
                    cryptoStream.Close();
                    memory.Close();
                    result = bytes;
                }
            }
        }

        return result;
    }

    public byte[] Decrypt(byte[] input, string key)
    {
        var result = default(byte[]);

        var salt = input.Take(IvSize).ToArray();
        var iv = input.Skip(IvSize).Take(IvSize).ToArray();
        var cipherBytes = input.Skip(IvSize * 2).Take(input.Length - (2 * IvSize)).ToArray();
        var keyBytes = Rfc2898DeriveBytes.Pbkdf2(key, salt, 1000, HashAlgorithmName.SHA256, IvSize);

        using var algorithm = GetAlgorithm();
        using var decryptor = algorithm.CreateDecryptor(keyBytes, iv);
        using (var memory = new MemoryStream(cipherBytes))
        {
            using (var cryptoStream = new CryptoStream(memory, decryptor, CryptoStreamMode.Read))
            {
                using (var stream = new MemoryStream())
                {
                    cryptoStream.CopyTo(stream);
                    result = stream.ToArray();
                }
            }
        }

        return result;
    }
}