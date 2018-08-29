using Caishen.Internals.KeyCreators.StringEncoders;
using Caishen.Tests.Internals.KeyCreators.HashAlgorithms;
using Xunit;

namespace Caishen.Tests.Internals.KeyCreators.StringEncoders
{
    public class HexidecimalStringEncoderTests
    {
        private readonly HexadecimalStringEncoder _sut = new HexadecimalStringEncoder();

        [Fact]
        public void BytesToStringTest()
        {
            // Given
            var input = Util.StringToByteArray("61 6c 61 20 31 32 33");
            var expected = "616c6120313233";
            
            // When
            var result = _sut.BytesToString(input);
            
            // THen
            Assert.Equal(expected, result);
        }
    }
}