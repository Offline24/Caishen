using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Caishen.Internals.KeyCreators;

namespace Caishen.Benchmarks.Internals.KeyCreators
{
    public class KeyCreatingCreateKeyBenchmarks
    {
        private object[] _argument;
        private IKeyCreator _binaryFormatterSha256Base64KeyCreator;
        
        [GlobalSetup]
        public void Setup()
        {
            _argument = new object[] {1, "abc", DateTime.Now, new List<decimal>{1m, 11.7m}};
            _binaryFormatterSha256Base64KeyCreator = KeyCreator.BinaryFormatterSha256Base64KeyCreator;
        }

        [Benchmark]
        public string BinaryFormatterSha256Base64KeyCreator() => _binaryFormatterSha256Base64KeyCreator.CreateKey(_argument);
    }
}