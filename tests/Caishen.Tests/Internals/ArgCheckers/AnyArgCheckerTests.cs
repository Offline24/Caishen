using Caishen.Internals.ArgCheckers;
using Xunit;
// ReSharper disable ExpressionIsAlwaysNull

namespace Caishen.Tests.Internals.ArgCheckers
{
    public class AnyArgCheckerTests
    {
        private readonly AnyArgChecker _sut = new AnyArgChecker();

        [Fact]
        public void MatchReturnTrueForNull()
        {
            // Given
            object argument = null;
            
            // When
            var result = _sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
        
        [Fact]
        public void MatchAlwaysReturnTrueForValueTypes()
        {
            // Given
            var argument = 10;
            
            // When
            var result = _sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
        
        [Fact]
        public void MatchAlwaysReturnTrueForReferenceTypes()
        {
            // Given
            var argument = new object();
            
            // When
            var result = _sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
    }
}