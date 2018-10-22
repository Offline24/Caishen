using System.Linq;
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
        public void FindMatchingItems_EmptyItems()
        {
            // Given
            var items = new (MethodInfo methodInfo, ISingleMethodCallChecker checker, int item)[]{};
            var sut = new CollectionWithCallMatcher<int>(items);

            // When
            var result = sut.FindMatchingItems(GetStringWithParametersMethodInfo, new object[] { });
            
            // Then
            Assert.Empty(result);
        }
        
        [Fact]
        public void FindMatchingItems_OneItemPerMethod()
        {
            // Given
            var items = new (MethodInfo methodInfo, ISingleMethodCallChecker checker, int item)[]
            {
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(true), 2),
                (GetIntNoParametersMethodInfo, new MockSingleMethodCallChecker(true), 3),
            };
            var sut = new CollectionWithCallMatcher<int>(items);

            // When
            var result1 = sut.FindMatchingItems(GetStringWithParametersMethodInfo, new object[] { });
            var result2 = sut.FindMatchingItems(GetIntNoParametersMethodInfo, new object[] { });
            var result3 = sut.FindMatchingItems(GetIntWithParametersMethodInfo, new object[] { });
            
            // Then
            var value1 = Assert.Single(result1);
            Assert.Equal(2, value1);
            
            var value2 = Assert.Single(result2);
            Assert.Equal(3, value2);

            Assert.Empty(result3);
        }
        
        [Fact]
        public void FindMatchingItems_MultipleItemsPerMethodButOnlyOneMatching()
        {
            // Given
            var items = new (MethodInfo methodInfo, ISingleMethodCallChecker checker, int item)[]
            {
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 2),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 3),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(true), 4),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 5),
            };
            var sut = new CollectionWithCallMatcher<int>(items);

            // When
            var result = sut.FindMatchingItems(GetStringWithParametersMethodInfo, new object[] { });
            
            // Then
            var value = Assert.Single(result);
            Assert.Equal(4, value);
        }
        
        [Fact]
        public void FindMatchingItems_MultipleItemsPerMethod()
        {
            // Given
            var items = new (MethodInfo methodInfo, ISingleMethodCallChecker checker, int item)[]
            {
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 2),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(true), 3),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(true), 4),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 5),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(true), 6),

            };
            var sut = new CollectionWithCallMatcher<int>(items);

            // When
            var result = sut.FindMatchingItems(GetStringWithParametersMethodInfo, new object[] { }).ToList();
            
            // Then
            Assert.Equal(3, result.Count);
            Assert.Contains(3, result);
            Assert.Contains(4, result);
            Assert.Contains(6, result);
        }
        
        [Fact]
        public void FindMatchingItems_NoneMatch()
        {
            // Given
            var items = new (MethodInfo methodInfo, ISingleMethodCallChecker checker, int item)[]
            {
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 2),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 3),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 4),
                (GetStringWithParametersMethodInfo, new MockSingleMethodCallChecker(false), 5),
            };
            var sut = new CollectionWithCallMatcher<int>(items);

            // When
            var result = sut.FindMatchingItems(GetStringWithParametersMethodInfo, new object[] { });
            
            // Then
            Assert.Empty(result);
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