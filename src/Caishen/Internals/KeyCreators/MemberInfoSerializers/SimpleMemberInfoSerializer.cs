using System.Reflection;
using System.Text;

namespace Caishen.Internals.KeyCreators.MemberInfoSerializers
{
    internal class SimpleMemberInfoSerializer : IMemberInfoSerializer
    {
        public byte[] Serialize(MemberInfo memberInfo)
        {
            var stringKey = memberInfo.DeclaringType?.FullName + memberInfo.Name;
            return Encoding.ASCII.GetBytes(stringKey);
        }
    }
}