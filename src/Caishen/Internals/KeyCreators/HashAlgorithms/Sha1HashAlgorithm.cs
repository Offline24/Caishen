using System.Security.Cryptography;

namespace Caishen.Internals.KeyCreators.HashAlgorithms
{
    internal class Sha1HashAlgorithm : IHashAlgorithm
    {
        public byte[] CreateHash(byte[] bytes)
        {
            using (var sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(bytes);
            }
        }
    }
}