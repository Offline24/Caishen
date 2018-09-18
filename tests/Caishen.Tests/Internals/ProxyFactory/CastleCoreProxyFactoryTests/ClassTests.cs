using Caishen.Internals;
using Caishen.Internals.ProxyFactories;
using Caishen.Tests.TestUtilities;
using Xunit;

namespace Caishen.Tests.Internals.ProxyFactory.CastleCoreProxyFactoryTests
{
    public class ClassTests
    {
        private readonly CastleCoreProxyFactory<ITestClass>_sut = new CastleCoreProxyFactory<ITestClass>();

        [Fact]
        public void CannotCreateFactoryForNonInterface()
        {
            var exception = Assert.Throws<CaishenException>(() => new CastleCoreProxyFactory<TestClass>());
            Assert.Equal(CaishenExceptionReason.CannotCreateProxyForNonInterface, exception.Reason);
        }
        
        [Fact]
        public void ShouldWrapCall()
        {
            // Given
            var testClass = new TestClass();
            var proxyObject = _sut.CreateProxy(testClass);
            
            // When
            var result = proxyObject.GetNextInt(2);
            
            // Then
            Assert.Equal(3, result);
        }
        
        [Fact]
        public void ShouldWrapFields()
        {
            // Given
            var testClass = new TestClass();
            var proxyObject = _sut.CreateProxy(testClass);

            // Then
            Assert.Equal(2, proxyObject.Two);
        }
    }
}