using Caishen.Internals;
using Caishen.Internals.ProxyFactories;
using Caishen.Tests.TestUtilities;
using Xunit;

namespace Caishen.Tests.Internals.ProxyFactories.CastleCoreProxyFactoryTests
{
    public class ClassWithStateTests
    {
        private readonly IProxyFactory<ITestClassWithState> _sutForInterface = new CastleCoreProxyFactory<ITestClassWithState>();
        
        [Fact]
        public void CannotCreateFactoryForNonInterface()
        {
            var exception = Assert.Throws<CaishenException>(() => new CastleCoreProxyFactory<TestClassWithState>());
            Assert.Equal(CaishenExceptionReason.CannotCreateProxyForNonInterface, exception.Reason);
        }
        
        [Fact]
        public void ShouldWrapCall()
        {
            // Given
            var testClassWithState = new TestClassWithState();
            var proxyObject = _sutForInterface.CreateProxy(testClassWithState);
            
            // When
            var firstCallResult = proxyObject.GetNextInt();
            var secondCallResult = proxyObject.GetNextInt();
            
            // Then
            Assert.Equal(0, firstCallResult);
            Assert.Equal(1, secondCallResult);
        }
        
        [Fact]
        public void UnderlyingObjectShouldChangeState()
        {
            // Given
            var testClassWithState = new TestClassWithState();
            var proxyObject = _sutForInterface.CreateProxy(testClassWithState);
            
            // When
            proxyObject.GetNextInt();
            proxyObject.GetNextInt();
            
            // Then
            Assert.Equal(2, testClassWithState.InternalState);
        }
    }
}