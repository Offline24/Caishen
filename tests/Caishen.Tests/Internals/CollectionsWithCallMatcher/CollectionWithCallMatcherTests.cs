using System.Reflection;
using Caishen.Internals.CallCheckers;
using Caishen.Internals.CollectionsWithCallMatcher;
using Caishen.Tests.TestUtilities;
using Xunit;

namespace Caishen.Tests.Internals.CollectionsWithCallMatcher
{
    public class CollectionWithCallMatcherTests
    {
        private MethodInfo GetStringWithParametersMethodInfo =>
            typeof(TestClassWithFunctions).GetMethod(nameof(TestClassWithFunctions.GetStringWithParameters));
        private MethodInfo GetIntNoParametersMethodInfo =>
            typeof(TestClassWithFunctions).GetMethod(nameof(TestClassWithFunctions.GetIntNoParameters));
        private MethodInfo GetIntWithParametersMethodInfo =>
            typeof(TestClassWithFunctions).GetMethod(nameof(TestClassWithFunctions.GetIntWithParameters));
        
        [Fact]
        public void FindMatchingItemEmptyItems()
        {
            // Given
            var defaultValue = 1;
            var items = new (MethodInfo methodInfo, ISingleMethodCallChecker checker, int item)[]{};
            var sut = new CollectionWithCallMatcher<int>(defaultValue, items);

            // When
            var result = sut.FindMatchingItem(GetStringWithParametersMethodInfo, new object[] { });
            
            // Then
            Assert.Equal(defaultValue, result);
        }
        
        [Fact]
        public void FindMatchingItemOneItemPerMethod()
        {
            // Given
            var defaultValue = 1;
            var items = new (MethodInfo methodInfo, ISingleMethodCallChecker checker, int item)[]
            {
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(true), 2),
                (GetIntNoParametersMethodInfo, new MockSingleMethodCallChecker(true), 3),
            };
            var sut = new CollectionWithCallMatcher<int>(defaultValue, items);

            // When
            var result1 = sut.FindMatchingItem(GetStringWithParametersMethodInfo, new object[] { });
            var result2 = sut.FindMatchingItem(GetIntNoParametersMethodInfo, new object[] { });
            var result3 = sut.FindMatchingItem(GetIntWithParametersMethodInfo, new object[] { });
            
            // Then
            Assert.Equal(2, result1);
            Assert.Equal(3, result2);
            Assert.Equal(1, result3);
        }
        
        [Fact]
        public void FindMatchingItemMultipleItemsPerMethod()
        {
            // Given
            var defaultValue = 1;
            var items = new (MethodInfo methodInfo, ISingleMethodCallChecker checker, int item)[]
            {
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 2),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 3),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(true), 4),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 5),
            };
            var sut = new CollectionWithCallMatcher<int>(defaultValue, items);

            // When
            var result = sut.FindMatchingItem(GetStringWithParametersMethodInfo, new object[] { });
            
            // Then
            Assert.Equal(4, result);
        }
        
        [Fact]
        public void FindMatchingItemDefaultValue()
        {
            // Given
            var defaultValue = 1;
            var items = new (MethodInfo methodInfo, ISingleMethodCallChecker checker, int item)[]
            {
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 2),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 3),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 4),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 5),
            };
            var sut = new CollectionWithCallMatcher<int>(defaultValue, items);

            // When
            var result = sut.FindMatchingItem(GetStringWithParametersMethodInfo, new object[] { });
            
            // Then
            Assert.Equal(defaultValue, result);
        }

        private class MockSingleMethodCallChecker : ISingleMethodCallChecker
        {
            private readonly bool _returnValue;

            public MockSingleMethodCallChecker(bool returnValue)
            {
                _returnValue = returnValue;
            }

            public bool DoesCallMatch(object[] arguments) => _returnValue;
        }
    }
}