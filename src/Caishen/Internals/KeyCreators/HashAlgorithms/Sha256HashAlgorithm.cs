using System.Security.Cryptography;

namespace Caishen.Internals.KeyCreators.HashAlgorithms
{
    internal class Sha256HashAlgorithm : IHashAlgorithm
    {
        public byte[] CreateHash(byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(bytes);
            }
        }
    }
}