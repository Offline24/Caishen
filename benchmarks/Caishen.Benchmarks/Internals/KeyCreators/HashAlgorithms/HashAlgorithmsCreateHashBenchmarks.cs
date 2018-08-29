using System.Text;
using BenchmarkDotNet.Attributes;
using Caishen.Internals.KeyCreators.HashAlgorithms;

namespace Caishen.Benchmarks.Internals.KeyCreators.HashAlgorithms
{
    public class HashAlgorithmsCreateHashBenchmarks
    {
        private byte[] _bytes;
        private IHashAlgorithm _md5, _sha1, _sha256;
        
        [GlobalSetup]
        public void Setup()
        {
            _bytes = Encoding.UTF8.GetBytes("qwertyuiop1234567890asdfghjklzxcvbnm");
            _md5 = new Md5HashAlgorithm();
            _sha1 = new Sha1HashAlgorithm();
            _sha256 = new Sha256HashAlgorithm();
        }

        [Benchmark]
        public byte[] Md5HashAlgorithm() => _md5.CreateHash(_bytes);
        
        [Benchmark]
        public byte[] Sha1HashAlgorithm() => _sha1.CreateHash(_bytes);
        
        [Benchmark]
        public byte[] Sha256HashAlgorithm() => _sha256.CreateHash(_bytes);
    }
}