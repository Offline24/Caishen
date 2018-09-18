using System.Reflection;

namespace Caishen.Internals.CollectionsWithCallMatcher
{
    internal interface ICollectionWithCallMatcher<out TItem>
    {
        TItem FindMatchingItem(MethodInfo methodInfo, object[] arguments);
    }
}