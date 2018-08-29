using Caishen.Internals.ProxyFactory;

namespace Caishen.Internals
{
    internal class InternalCacheFactory<TInterface> : ICacheFactory<TInterface> where TInterface : class
    {
        private readonly IProxyFactory<TInterface> _proxyFactory;

        public InternalCacheFactory(IProxyFactory<TInterface> proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }

        public TInterface Create(TInterface underlyingObject)
        {
            return _proxyFactory.CreateProxy(underlyingObject);
        }
    }
}