namespace Caishen.Internals.ArgCheckers
{
    internal class AnyArgChecker : IArgChecker
    {
        public bool Match(object argument) => true;
    }
}