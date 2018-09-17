namespace Caishen.Internals.ArgCheckers
{
    internal interface IArgChecker
    {
        bool Match(object argument);
    }
}