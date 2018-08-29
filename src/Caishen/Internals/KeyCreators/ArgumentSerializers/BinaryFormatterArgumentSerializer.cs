using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Caishen.Internals.KeyCreators.ArgumentSerializers
{
    public class BinaryFormatterArgumentSerializer : IArgumentSerializer
    {
        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
        
        public byte[] Serialize(object[] arguments)
        {
            using (var memoryStream = new MemoryStream())
            {
                foreach (var argument in arguments)
                {
                    _binaryFormatter.Serialize(memoryStream, argument);
                }

                return memoryStream.ToArray();
            }
        }
    }
}