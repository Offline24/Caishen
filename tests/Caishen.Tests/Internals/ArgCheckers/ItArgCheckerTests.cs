using Caishen.Internals.ArgCheckers;
using Xunit;

namespace Caishen.Tests.Internals.ArgCheckers
{
    public class ItArgCheckerTests
    {
        [Fact]
        public void MatchReturnTrueForNulls()
        {
            // Given
            object argument = null;
            var sut = new ItArgChecker(null);

            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
        
        [Fact]
        public void MatchReturnTrueForEqualObject()
        {
            // Given
            var argument = new object();
            var sut = new ItArgChecker(argument);

            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
        
        [Fact]
        public void MatchReturnFalseForDifferentObject()
        {
            // Given
            var argument = new object();
            var sut = new ItArgChecker(new object());

            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.False(result);
        }
        
        [Fact]
        public void MatchReturnTrueForEqualValues()
        {
            // Given
            var argument = 11;
            var sut = new ItArgChecker(11);

            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
        
        [Fact]
        public void MatchReturnTrueForEqualNullableValues()
        {
            // Given
            int? argument = 11;
            var sut = new ItArgChecker(new int?(11));

            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.True(result);
        }
        
        [Fact]
        public void MatchReturnFalseForDifferentValues()
        {
            // Given
            var argument = 12;
            var sut = new ItArgChecker(11);

            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.False(result);
        }
        
        [Fact]
        public void MatchReturnFalseForDifferentTypes()
        {
            // Given
            var argument = 11L;
            var sut = new ItArgChecker(11);

            // When
            var result = sut.Match(argument);
            
            // Then
            Assert.False(result);
        }
    }
}