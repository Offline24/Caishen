using System;
using System.Linq;
using System.Reflection;
using Caishen.Internals.KeyCreators.MemberInfoSerializers;
using Xunit;

namespace Caishen.Tests.Internals.KeyCreators.MemberInfoSerializers
{
    public class SimpleMemberInfoSerializerTests
    {
        private readonly SimpleMemberInfoSerializer _sut = new SimpleMemberInfoSerializer();

        [Fact]
        public void Serialize_Same()
        {
            var args1 = MethodBase.GetCurrentMethod();
            var args2 = MethodBase.GetCurrentMethod();

            var bytes1 = _sut.Serialize(args1);
            var bytes2 = _sut.Serialize(args2);

            Assert.Equal(bytes1.AsEnumerable(), bytes2.AsEnumerable());
        }
        
        /// <summary>
        /// The difference in arguments will be detected by IArgumentSerializer <see cref="KeyCreatorTests.Default_DifferentOverload"/>
        /// </summary>
        [Fact]
        public void Serialize_Same2()
        {
            var args1 = typeof(TestClassC).GetMethod(nameof(TestClassC.Method), new Type[] {});
            var args2 = typeof(TestClassC).GetMethod(nameof(TestClassC.Method), new[] { typeof(int)});
            
            var bytes1 = _sut.Serialize(args1);
            var bytes2 = _sut.Serialize(args2);

            Assert.Equal(bytes1.AsEnumerable(), bytes2.AsEnumerable());
        }
        
        [Fact]
        public void Serialize_Different()
        {
            MethodBase CreateMethodBase1() => MethodBase.GetCurrentMethod();
            MethodBase CreateMethodBase2() => MethodBase.GetCurrentMethod();
            
            var args1 = CreateMethodBase1();
            var args2 = CreateMethodBase2();

            var bytes1 = _sut.Serialize(args1);
            var bytes2 = _sut.Serialize(args2);

            Assert.NotEqual(bytes1.AsEnumerable(), bytes2.AsEnumerable());
        }
        
        [Fact]
        public void Serialize_Different2()
        {
            var args1 = typeof(TestClassA).GetMethod(nameof(TestClassA.Method));
            var args2 = typeof(TestClassB).GetMethod(nameof(TestClassB.Method));

            var bytes1 = _sut.Serialize(args1);
            var bytes2 = _sut.Serialize(args2);

            Assert.NotEqual(bytes1.AsEnumerable(), bytes2.AsEnumerable());
        }

        private class TestClassA
        {
            public int Method() => 1;
        }
        
        private class TestClassB
        {
            public int Method() => 1;
        }

        private class TestClassC
        {
            public int Method() => 0;
            public int Method(int x) => x;
        }
    }
}