using System.Text;

namespace Caishen.Internals.KeyCreators.StringEncoders
{
    internal class HexadecimalStringEncoder : IStringEncoder
    {
        public string BytesToString(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (byte oneByte in bytes)
            {
                sb.AppendFormat("{0:x2}", oneByte);
            }
            
            return sb.ToString();
        }
    }
}