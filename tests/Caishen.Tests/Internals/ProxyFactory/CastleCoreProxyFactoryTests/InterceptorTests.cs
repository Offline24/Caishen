using Caishen.Internals.ProxyFactory;
using Caishen.Tests.TestUtilities;
using Xunit;

namespace Caishen.Tests.Internals.ProxyFactory.CastleCoreProxyFactoryTests
{
    public class InterceptorTests
    {
        [Fact]
        public void ShouldIntercept()
        {
            // Given
            var testClass = new TestClass();
            var interceptor = new LastInvocationInterceptor();
            var factory = new CastleCoreProxyFactory<ITestClass>();
            var proxy = factory.CreateProxy(testClass, interceptor);
            
            // When
            proxy.GetNextInt(10);
            
            // Then
            var invocation = interceptor.LastInvocationParameter;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(testClass.GetNextInt), invocation.Method.Name);
        }
        
        [Fact]
        public void ShouldPassArguments()
        {
            // Given
            var testClass = new TestClass();
            var interceptor = new LastInvocationInterceptor();
            var factory = new CastleCoreProxyFactory<ITestClass>();
            var proxy = factory.CreateProxy(testClass, interceptor);
            
            // When
            proxy.GetNextInt(10);
            
            // Then
            var invocation = interceptor.LastInvocationParameter;
            Assert.NotNull(invocation);
            var argument = Assert.Single(invocation.Arguments);
            Assert.Equal(10, argument);
        }
        
        [Fact]
        public void ShouldBePossibleToOverwriteValue()
        {
            // Given
            var testClass = new TestClass();
            var interceptor = new ValueOverwritingInterceptor(20);
            var factory = new CastleCoreProxyFactory<ITestClass>();
            var proxy = factory.CreateProxy(testClass, interceptor);
            
            // When
            var result = proxy.GetNextInt(10);
            
            // Then
            Assert.Equal(20, result);
        }
        
        [Fact]
        public void IfNotInterruptShouldCallUnderlyingObjectMethod()
        {
            // Given
            var testClass = new TestClass();
            var interceptor = new LastInvocationInterceptor();
            var factory = new CastleCoreProxyFactory<ITestClass>();
            var proxy = factory.CreateProxy(testClass, interceptor);
            
            // When
            var result = proxy.GetNextInt(10);
            
            // Then
            Assert.Equal(11, result);
        }

        private class LastInvocationInterceptor : IInterceptor
        {
            public IInvocation LastInvocationParameter { get; private set; }
            
            public void Intercept(IInvocation invocation)
            {
                LastInvocationParameter = invocation;
                invocation.Proceed();
            }
        }

        private class ValueOverwritingInterceptor : IInterceptor
        {
            private readonly object _value;

            public ValueOverwritingInterceptor(object value)
            {
                _value = value;
            }

            public void Intercept(IInvocation invocation)
            {
                invocation.ReturnValue = _value;
            }
        }
    }
}