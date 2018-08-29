using System.Collections.Generic;
using System.Linq;
using Caishen.Internals.KeyCreators.ArgumentSerializers;
using Xunit;

namespace Caishen.Tests.Internals.KeyCreators.ArgumentSerializers
{
    public class BinaryFormatterArgumentSerializerTests
    {
        private readonly BinaryFormatterArgumentSerializer _sut = new BinaryFormatterArgumentSerializer();

        [Fact]
        public void Serialize_Same()
        {
            var args1 = new object[] { "abc", 1 };
            var args2 = new object[] { "abc", 1 };

            var bytes1 = _sut.Serialize(args1);
            var bytes2 = _sut.Serialize(args2);

            Assert.Equal(bytes1.AsEnumerable(), bytes2.AsEnumerable());
        }
        
        [Fact]
        public void Serialize_ArgumentOrder()
        {
            var args1 = new object[] { 1, 2 };
            var args2 = new object[] { 2, 1 };

            var bytes1 = _sut.Serialize(args1);
            var bytes2 = _sut.Serialize(args2);

            Assert.NotEqual(bytes1.AsEnumerable(), bytes2.AsEnumerable());
        }
        
        [Fact]
        public void Serialize_Lists()
        {
            var args1 = new object[] { new List<int>{ 1, 2 } };
            var args2 = new object[] { new List<int>{ 2, 3 } };

            var bytes1 = _sut.Serialize(args1);
            var bytes2 = _sut.Serialize(args2);

            Assert.NotEqual(bytes1.AsEnumerable(), bytes2.AsEnumerable());
        }
    }
}