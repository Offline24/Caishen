using System.Text;
using BenchmarkDotNet.Attributes;
using Caishen.Internals.KeyCreators.StringEncoders;

namespace Caishen.Benchmarks.Internals.KeyCreators.StringEncoders
{
    public class StringEncodersBytesToStringBenchmarks
    {
        private byte[] _bytes;
        private IStringEncoder _base64, _hexadecimal;
        
        [GlobalSetup]
        public void Setup()
        {
            _bytes = Encoding.UTF8.GetBytes("qwertyuiop1234567890asdfghjklzxcvbnm");
            _base64 = new Base64StringEncoder();
            _hexadecimal = new HexadecimalStringEncoder();
        }

        [Benchmark]
        public string Base64StringEncoder() => _base64.BytesToString(_bytes);

        [Benchmark]
        public string HexadecimalStringEncoder() => _hexadecimal.BytesToString(_bytes);
    }
}