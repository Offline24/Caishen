namespace Caishen.Internals.ProxyFactories
{
    internal interface IInterceptor
    {
        void Intercept(IInvocation invocation);
    }
}