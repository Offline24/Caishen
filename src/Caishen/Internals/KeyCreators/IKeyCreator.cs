using System.Reflection;

namespace Caishen.Internals.KeyCreators
{
    public interface IKeyCreator
    {
        string CreateKey(MemberInfo memberInfo, object[] arguments);
    }
}