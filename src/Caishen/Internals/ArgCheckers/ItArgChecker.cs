namespace Caishen.Internals.ArgCheckers
{
    internal class ItArgChecker : IArgChecker
    {
        private readonly object _value;

        public ItArgChecker(object value)
        {
            _value = value;
        }

        public bool Match(object argument)
        {
            if (argument == null)
            {
                return _value == null;
            }
            
            if (argument.GetType().IsValueType)
            {
                return argument.Equals(_value);
            }

            return argument == _value;
        }
    }
}