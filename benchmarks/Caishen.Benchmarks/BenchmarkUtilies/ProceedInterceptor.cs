using Caishen.Internals.ProxyFactories;

namespace Caishen.Benchmarks.BenchmarkUtilies
{
    public class ProceedInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}