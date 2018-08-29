using System;
using System.Linq;

namespace Caishen.Tests.Internals.KeyCreators.HashAlgorithms
{
    public static class Util
    {
        public static byte[] StringToByteArray(string hex)
        {
            var input = hex.Replace(" ", "");
            return Enumerable.Range(0, input.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(input.Substring(x, 2), 16))
                .ToArray();
        }
    }
}