using System.Security.Cryptography;

namespace Caishen.Internals.KeyCreators.HashAlgorithms
{
    internal class Md5HashAlgorithm : IHashAlgorithm
    {
        public byte[] CreateHash(byte[] bytes)
        {
            using (var md5Hash = MD5.Create())
            {
                return md5Hash.ComputeHash(bytes);
            }
        }
    }
}