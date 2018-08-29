namespace Caishen.Internals.ProxyFactory
{
    internal interface IInterceptor
    {
        void Intercept(IInvocation invocation);
    }
}