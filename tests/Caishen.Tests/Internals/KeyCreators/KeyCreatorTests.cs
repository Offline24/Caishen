using System.Collections.Generic;
using Caishen.Internals.KeyCreators;
using Xunit;

namespace Caishen.Tests.Internals.KeyCreators
{
    public class KeyCreatorTests
    {
        [Fact]
        public void BinaryFormatterSha256Base64KeyCreator_Same()
        {
            // Given
            var sut = KeyCreator.BinaryFormatterSha256Base64KeyCreator;
            var input = new object[] {1, "abc", new List<int> {2, 3}};
            
            // When
            var result1 = sut.CreateKey(input);
            var result2 = sut.CreateKey(input);
            
            // Then
            Assert.Equal(result1, result2);
        }
        
        [Fact]
        public void BinaryFormatterSha256Base64KeyCreator_DifferentOrder()
        {
            // Given
            var sut = KeyCreator.BinaryFormatterSha256Base64KeyCreator;
            var input1 = new object[] {1, "abc", new List<int> {2, 3}};
            var input2 = new object[] {"abc", 1, new List<int> {2, 3}};
            
            // When
            var result1 = sut.CreateKey(input1);
            var result2 = sut.CreateKey(input2);
            
            // Then
            Assert.NotEqual(result1, result2);
        }
        
        [Fact]
        public void BinaryFormatterSha256Base64KeyCreator_DifferentArguments()
        {
            // Given
            var sut = KeyCreator.BinaryFormatterSha256Base64KeyCreator;
            var input1 = new object[] {1, "abc", new List<int> {2, 3}};
            var input2 = new object[] {1, "abc", new List<int> {1, 3}};
            
            // When
            var result1 = sut.CreateKey(input1);
            var result2 = sut.CreateKey(input2);
            
            // Then
            Assert.NotEqual(result1, result2);
        }
        
        [Fact]
        public void BinaryFormatterSha256Base64KeyCreator_DifferentArguments2()
        {
            // Given
            var sut = KeyCreator.BinaryFormatterSha256Base64KeyCreator;
            var input1 = new object[] {"1"};
            var input2 = new object[] {1};
            
            // When
            var result1 = sut.CreateKey(input1);
            var result2 = sut.CreateKey(input2);
            
            // Then
            Assert.NotEqual(result1, result2);
        }
    }
}