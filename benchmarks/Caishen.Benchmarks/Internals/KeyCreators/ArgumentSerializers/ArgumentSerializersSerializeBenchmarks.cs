using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Caishen.Internals.KeyCreators.ArgumentSerializers;

namespace Caishen.Benchmarks.Internals.KeyCreators.ArgumentSerializers
{
    public class ArgumentSerializersSerializeBenchmarks
    {
        private object[] _argument;
        private IArgumentSerializer _binaryFormatterArgumentSerializer;
        
        [GlobalSetup]
        public void Setup()
        {
            _argument = new object[] {1, "abc", DateTime.Now, new List<decimal>{1m, 11.7m}};
            _binaryFormatterArgumentSerializer = new BinaryFormatterArgumentSerializer();
        }

        [Benchmark]
        public byte[] BinaryFormatterArgumentSerializer() => _binaryFormatterArgumentSerializer.Serialize(_argument);
    }
}