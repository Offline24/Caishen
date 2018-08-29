using Caishen.Internals;
using Caishen.Internals.ProxyFactory;

namespace Caishen
{
    public class CacheFactory<TInterface> : ICacheFactory<TInterface> where TInterface : class
    {
        private readonly ICacheFactory<TInterface> _internalCacheFactory;

        public CacheFactory()
        {
            var proxyFactory = new CastleCoreProxyFactory<TInterface>();
            
            _internalCacheFactory = new InternalCacheFactory<TInterface>(proxyFactory);
        }
        
        public TInterface Create(TInterface underlyingObject)
        {
            return _internalCacheFactory.Create(underlyingObject);
        }
    }
}