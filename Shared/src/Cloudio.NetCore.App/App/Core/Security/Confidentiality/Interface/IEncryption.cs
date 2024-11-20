namespace Cloudio.Core.Security;

public interface IEncryption
{
    byte[] Encrypt(byte[] input, string key);
    byte[] Decrypt(byte[] input, string key);
}
