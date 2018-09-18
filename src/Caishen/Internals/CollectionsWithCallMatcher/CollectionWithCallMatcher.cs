using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Caishen.Internals.CallCheckers;

namespace Caishen.Internals.CollectionsWithCallMatcher
{
    internal class CollectionWithCallMatcher<TItem> : ICollectionWithCallMatcher<TItem>
    {
        private readonly TItem _defaultValue;
        private readonly Dictionary<MethodInfo, (ISingleMethodCallChecker checker, TItem item)[]> _data;

        public CollectionWithCallMatcher(
            TItem defaultValue,
            IEnumerable<(MethodInfo methodInfo, ISingleMethodCallChecker checker, TItem item)> items)
        {
            _defaultValue = defaultValue;
            _data = items
                .GroupBy(x => x.methodInfo, x => (x.checker, x.item))
                .ToDictionary(x => x.Key, x => x.ToArray());
        }
        
        public TItem FindMatchingItem(MethodInfo methodInfo, object[] arguments)
        {
            if (_data.ContainsKey(methodInfo))
            {
                var checkersWithItems = _data[methodInfo];
                for (var i = 0; i < checkersWithItems.Length; i++)
                {
                    var checkerWithItem = checkersWithItems[i];
                    if (checkerWithItem.checker.DoesCallMatch(arguments))
                    {
                        return checkerWithItem.item;
                    }
                }
            }

            return _defaultValue;
        }
    }
}