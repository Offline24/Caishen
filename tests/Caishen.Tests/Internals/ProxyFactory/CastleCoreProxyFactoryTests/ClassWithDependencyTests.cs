using Caishen.Internals;
using Caishen.Internals.ProxyFactory;
using Caishen.Tests.TestUtilities;
using Xunit;

namespace Caishen.Tests.Internals.ProxyFactory.CastleCoreProxyFactoryTests
{
    public class TestClassWithDependencyTests
    {
        private readonly IProxyFactory<ITestClassWithDependency> _sutForInterface = new CastleCoreProxyFactory<ITestClassWithDependency>();
        
        [Fact]
        public void CannotCreateFactoryForNonInterface()
        {
            var exception = Assert.Throws<CaishenException>(() => new CastleCoreProxyFactory<TestClassWithDependency>());
            Assert.Equal(CaishenExceptionReason.CannotCreateProxyForNonInterface, exception.Reason);
        }

        [Fact]
        public void ShouldWrapCall()
        {
            // Given
            ITestClassWithDependency testClassWithState = TestClassWithDependency.Create();
            var proxyObject = _sutForInterface.CreateProxy(testClassWithState);
            
            // When
            var result = proxyObject.GetNextInt(2);
            
            // Then
            Assert.Equal(3, result);
        }
    }
}