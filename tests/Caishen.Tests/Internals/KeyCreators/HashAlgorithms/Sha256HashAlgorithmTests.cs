using Caishen.Internals.KeyCreators.HashAlgorithms;
using Xunit;

namespace Caishen.Tests.Internals.KeyCreators.HashAlgorithms
{
    public class Sha256HashAlgorithmTests
    {
        private readonly Sha256HashAlgorithm _sut = new Sha256HashAlgorithm(); 
        
        [Fact]
        public void CreateHashTest()
        {
            // Given
            var input = Util.StringToByteArray("61 6c 61 20 31 32 33");
            var expectedResult = Util.StringToByteArray("24 18 7e d0 f1 4a 49 1e 15 6f d8 46 78 94 69 f7 dd 49 17 79 47 10 c9 8e d9 db ca 54 64 35 a3 a1");
            
            // When
            var result = _sut.CreateHash(input);
            
            // Then
            Assert.Equal(expectedResult, result);
        }
    }
}