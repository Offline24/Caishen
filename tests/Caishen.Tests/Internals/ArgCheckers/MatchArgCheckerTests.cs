using Caishen.Internals.ArgCheckers;
using Xunit;
// ReSharper disable ExpressionIsAlwaysNull

namespace Caishen.Tests.Internals.ArgCheckers
{
    public class MatchArgCheckerTests
    {
        [Fact]
        public void MatchReturnTrueForSatisfyingExpression()
        {
            // Given
            var argument = 10;
            var sut = new MatchArgChecker<int>(x => x > 5);
            
            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
        
        [Fact]
        public void MatchReturnFalseForNonSatisfyingExpression()
        {
            // Given
            var argument = 1;
            var sut = new MatchArgChecker<int>(x => x > 5);
            
            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.False(result);
        }
        
        [Fact]
        public void MatchReturnTrueForNullableSatisfyingExpression1()
        {
            // Given
            int? argument = null;
            var sut = new MatchArgChecker<int?>(x => !x.HasValue);
            
            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
        
        [Fact]
        public void MatchReturnTrueForNullableSatisfyingExpression2()
        {
            // Given
            int? argument = 11;
            var sut = new MatchArgChecker<int?>(x => x.HasValue && x.Value == 11);
            
            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
        
        [Fact]
        public void MatchReturnFalseForNullableInvalidTypes()
        {
            // Given
            int? argument = 11;
            var sut = new MatchArgChecker<long?>(x => x.HasValue && x.Value == 11);
            
            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.False(result);
        }
        
        [Fact]
        public void MatchReturnFalseForClassInvalidTypes()
        {
            // Given
            var argument = "";
            var sut = new MatchArgChecker<object>(x => x != null);
            
            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.False(result);
        }
        
        [Fact]
        public void MatchReturnTrueForClassSatisfyingExpression1()
        {
            // Given
            var argument = "";
            var sut = new MatchArgChecker<string>(x => x != null);
            
            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
        
        [Fact]
        public void MatchReturnTrueForClassSatisfyingExpression2()
        {
            // Given
            string argument = null;
            var sut = new MatchArgChecker<string>(x => x == null);
            
            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
    }
}