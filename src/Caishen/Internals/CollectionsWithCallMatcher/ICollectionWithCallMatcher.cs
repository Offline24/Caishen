using System.Collections.Generic;
using System.Reflection;

namespace Caishen.Internals.CollectionsWithCallMatcher
{
    internal interface ICollectionWithCallMatcher<out TItem>
    {
        IEnumerable<TItem> FindMatchingItems(MethodInfo methodInfo, object[] arguments);
    }
}