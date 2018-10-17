using System.Reflection;
using Caishen.Internals.KeyCreators.ArgumentSerializers;
using Caishen.Internals.KeyCreators.HashAlgorithms;
using Caishen.Internals.KeyCreators.MemberInfoSerializers;
using Caishen.Internals.KeyCreators.StringEncoders;

namespace Caishen.Internals.KeyCreators
{
    internal class KeyCreator : IKeyCreator
    {
        private readonly IArgumentSerializer _argumentSerializer;
        private readonly IMemberInfoSerializer _memberInfoSerializer;
        private readonly IHashAlgorithm _hashAlgorithm;
        private readonly IStringEncoder _stringEncoder;

        public static IKeyCreator Default => new KeyCreator(
            new BinaryFormatterArgumentSerializer(), 
            new SimpleMemberInfoSerializer(),
            new Sha256HashAlgorithm(), 
            new Base64StringEncoder());
        
        private KeyCreator(
            IArgumentSerializer argumentSerializer,
            IMemberInfoSerializer memberInfoSerializer,
            IHashAlgorithm hashAlgorithm,
            IStringEncoder stringEncoder)
        {
            _argumentSerializer = argumentSerializer;
            _hashAlgorithm = hashAlgorithm;
            _stringEncoder = stringEncoder;
            _memberInfoSerializer = memberInfoSerializer;
        }

        public string CreateKey(MemberInfo memberInfo, object[] arguments)
        {
            var serializedArguments = _argumentSerializer.Serialize(arguments);
            var serializedMemberInfo = _memberInfoSerializer.Serialize(memberInfo);

            var serialized = new byte[serializedArguments.Length + serializedMemberInfo.Length];
            serializedArguments.CopyTo(serialized, 0);
            serializedMemberInfo.CopyTo(serialized, serializedArguments.Length);
            
            var hashed = _hashAlgorithm.CreateHash(serialized);
            var key = _stringEncoder.BytesToString(hashed);

            return key;
        }
    }
}