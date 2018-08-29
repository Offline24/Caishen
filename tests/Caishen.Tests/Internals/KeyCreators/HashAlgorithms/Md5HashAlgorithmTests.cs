using Caishen.Internals.KeyCreators.HashAlgorithms;
using Xunit;

namespace Caishen.Tests.Internals.KeyCreators.HashAlgorithms
{
    public class Md5HashAlgorithmTests
    {
        private readonly Md5HashAlgorithm _sut = new Md5HashAlgorithm(); 
        
        [Fact]
        public void CreateHashTest()
        {
            // Given
            var input = Util.StringToByteArray("61 6c 61 20 31 32 33");
            var expectedResult = Util.StringToByteArray("5c ab b9 ea 10 88 bf b1 b2 be f2 c7 1d 09 44 99");
            
            // When
            var result = _sut.CreateHash(input);
            
            // Then
            Assert.Equal(expectedResult, result);
        }
    }
}