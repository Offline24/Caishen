namespace Caishen.Interfaces
{
    public interface IMethodCheckerConfiguration
    {
        IMethodCallChecker BuildCallChecker();
    }
}