using System.Collections.Generic;
using System.Linq;

namespace Caishen.Tests.TestUtilities
{
    public interface ICallCounterTestClass
    {
        int NextInt(int value);
    }

    public class CallCounterTestClass : ICallCounterTestClass
    {
        public IReadOnlyCollection<(int argument, int counter)> CallCounters =>
            _callCounters.Select(x => (x.Key, x.Value)).ToArray();
        
        private readonly IDictionary<int, int> _callCounters = new Dictionary<int, int>();

        public int NextInt(int value)
        {
            if (_callCounters.ContainsKey(value))
            {
                _callCounters[value] += 1;
            }
            else
            {
                _callCounters[value] = 1;
            }

            return value + 1;
        }
    }
}