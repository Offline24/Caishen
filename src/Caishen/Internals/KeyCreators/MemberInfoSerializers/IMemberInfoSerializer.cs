using System.Reflection;

namespace Caishen.Internals.KeyCreators.MemberInfoSerializers
{
    internal interface IMemberInfoSerializer
    {
        byte[] Serialize(MemberInfo memberInfo);
    }
}