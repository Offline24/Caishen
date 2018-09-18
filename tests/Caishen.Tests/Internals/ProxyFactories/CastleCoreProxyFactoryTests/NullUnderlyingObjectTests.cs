using System;
using System.Collections;
using Caishen.Internals;
using Caishen.Internals.ProxyFactories;
using Xunit;

namespace Caishen.Tests.Internals.ProxyFactories.CastleCoreProxyFactoryTests
{
    public class NullUnderlyingObjectTests
    {
        [Fact]
        public void ShouldThrowExceptionIfUnnderlyingObjectIsNull()
        {
            // Given
            var sut = new CastleCoreProxyFactory<ICollection>();
            
            // When
            Action action = () => sut.CreateProxy(null);
            
            // Then
            var exception = Assert.Throws<CaishenException>(action);
            Assert.Equal(CaishenExceptionReason.UnderlyingObjectIsNull, exception.Reason);
        }
    }
}