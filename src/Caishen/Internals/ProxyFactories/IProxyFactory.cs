namespace Caishen.Internals.ProxyFactories
{
    internal interface IProxyFactory<T> where T : class
    {
        T CreateProxy(T underlyingObject, params IInterceptor[] interceptors);
    }
}