using BenchmarkDotNet.Attributes;
using Caishen.Benchmarks.BenchmarkUtilies;
using Caishen.Internals.ProxyFactories;

namespace Caishen.Benchmarks.Internals.ProxyFactory
{
    public class ProxyFactoryMethodCallingBenchmarks
    {
        private int _argument;
        private INextIntInterface _object, _castleCoreProxy;
        
        [GlobalSetup]
        public void Setup()
        {
            _argument = 7;
            _object = new NextIntClass();
            
            var castleCoreFactory = new CastleCoreProxyFactory<INextIntInterface>();
            _castleCoreProxy = castleCoreFactory.CreateProxy(_object, new ProceedInterceptor());
        }

        [Benchmark]
        public int OriginalObject() => _object.GetNextInt(_argument);
        
        [Benchmark]
        public int CastleCoreProxy() => _castleCoreProxy.GetNextInt(_argument);
    }
}