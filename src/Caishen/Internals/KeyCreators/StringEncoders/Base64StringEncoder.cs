using System;

namespace Caishen.Internals.KeyCreators.StringEncoders
{
    internal class Base64StringEncoder : IStringEncoder
    {
        public string BytesToString(byte[] bytes) => Convert.ToBase64String(bytes);
    }
}