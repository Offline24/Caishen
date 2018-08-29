using Caishen.Internals.KeyCreators.HashAlgorithms;
using Xunit;

namespace Caishen.Tests.Internals.KeyCreators.HashAlgorithms
{
    public class Sha1HashAlgorithmTests
    {
        private readonly Sha1HashAlgorithm _sut = new Sha1HashAlgorithm(); 
        
        [Fact]
        public void CreateHashTest()
        {
            // Given
            var input = Util.StringToByteArray("61 6c 61 20 31 32 33");
            var expectedResult = Util.StringToByteArray("3a d3 cc ec 45 9e 29 e4 49 0f f2 10 71 33 23 82 82 c0 74 6f");
            
            // When
            var result = _sut.CreateHash(input);
            
            // Then
            Assert.Equal(expectedResult, result);
        }
    }
}