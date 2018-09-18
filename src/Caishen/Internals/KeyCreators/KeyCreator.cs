using Caishen.Internals.KeyCreators.ArgumentSerializers;
using Caishen.Internals.KeyCreators.HashAlgorithms;
using Caishen.Internals.KeyCreators.StringEncoders;

namespace Caishen.Internals.KeyCreators
{
    internal class KeyCreator : IKeyCreator
    {
        private readonly IArgumentSerializer _argumentSerializer;
        private readonly IHashAlgorithm _hashAlgorithm;
        private readonly IStringEncoder _stringEncoder;

        public static IKeyCreator BinaryFormatterSha256Base64KeyCreator => new KeyCreator(
            new BinaryFormatterArgumentSerializer(), 
            new Sha256HashAlgorithm(), 
            new Base64StringEncoder());
        
        private KeyCreator(
            IArgumentSerializer argumentSerializer,
            IHashAlgorithm hashAlgorithm,
            IStringEncoder stringEncoder)
        {
            _argumentSerializer = argumentSerializer;
            _hashAlgorithm = hashAlgorithm;
            _stringEncoder = stringEncoder;
        }

        public string CreateKey(object[] arguments)
        {
            var serialized = _argumentSerializer.Serialize(arguments);
            var hashed = _hashAlgorithm.CreateHash(serialized);
            var encoded = _stringEncoder.BytesToString(hashed);

            return encoded;
        }
    }
}