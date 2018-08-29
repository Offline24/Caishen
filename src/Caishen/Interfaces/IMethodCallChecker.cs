namespace Caishen.Interfaces
{
    public interface IMethodCallChecker
    {
        bool CheckCall(ICallDefinition callDefinition);
    }
}