namespace Cloudio.Core.Security;

public interface IHash
{
    string Compute(byte[] input);
    string Compute(string input);
}
