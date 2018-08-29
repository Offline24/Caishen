using Caishen.Internals.KeyCreators.StringEncoders;
using Caishen.Tests.Internals.KeyCreators.HashAlgorithms;
using Xunit;

namespace Caishen.Tests.Internals.KeyCreators.StringEncoders
{
    public class Base64StringEncoderTests
    {
        private readonly Base64StringEncoder _sut = new Base64StringEncoder();

        [Fact]
        public void BytesToStringTest()
        {
            // Given
            var input = Util.StringToByteArray("61 6c 61 20 31 32 33");
            var expected = "YWxhIDEyMw==";
            
            // When
            var result = _sut.BytesToString(input);
            
            // THen
            Assert.Equal(expected, result);
        }
    }
}