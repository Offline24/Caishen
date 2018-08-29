using BenchmarkDotNet.Attributes;
using Caishen.Internals.KeyCreators.HashAlgorithms;

namespace Caishen.Benchmarks.Internals.KeyCreators.HashAlgorithms
{
    public class HashAlgorithmsConstructorBenchmarks
    {
        [Benchmark]
        public object Md5HashAlgorithm() => new Md5HashAlgorithm();
        
        [Benchmark]
        public object Sha1HashAlgorithm() => new Sha1HashAlgorithm();
        
        [Benchmark]
        public object Sha256HashAlgorithm() => new Sha256HashAlgorithm();
    }
}