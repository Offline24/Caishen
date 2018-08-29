using BenchmarkDotNet.Attributes;
using Caishen.Internals.KeyCreators.StringEncoders;

namespace Caishen.Benchmarks.Internals.KeyCreators.StringEncoders
{
    public class StringEncodersConstructorBenchmarks
    {
        [Benchmark]
        public object Base64StringEncoder() => new Base64StringEncoder();
        
        [Benchmark]
        public object HexadecimalStringEncoder() => new HexadecimalStringEncoder();
    }
}