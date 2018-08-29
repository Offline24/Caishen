namespace Caishen.Internals.ProxyFactory
{
    internal interface IProxyFactory<T> where T : class
    {
        T CreateProxy(T underlyingObject, params IInterceptor[] interceptors);
    }
}