namespace Caishen.Internals.CallCheckers
{
    internal interface ISingleMethodCallChecker
    {
        bool DoesCallMatch(object[] arguments);
    }
}