using System;
using System.Collections.Generic;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using Caishen.Internals.KeyCreators;

namespace Caishen.Benchmarks.Internals.KeyCreators
{
    public class KeyCreatingCreateKeyBenchmarks
    {
        private MemberInfo _memberInfo;
        private object[] _argument;
        private IKeyCreator _default;
        
        [GlobalSetup]
        public void Setup()
        {
            _memberInfo = MethodBase.GetCurrentMethod();
            _argument = new object[] {1, "abc", DateTime.Now, new List<decimal>{1m, 11.7m}};
            _default = KeyCreator.Default;
        }

        [Benchmark]
        public string DefaultKeyCreator() => _default.CreateKey(_memberInfo, _argument);
    }
}