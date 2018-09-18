namespace Caishen.Tests.TestUtilities
{
    public interface ITestInterfaceWithFunctions
    {
        string GetStringWithParameters(int i, string s);
        int GetIntNoParameters();
        int GetIntWithParameters(int i, string s);
    }

    public class TestClassWithFunctions : ITestInterfaceWithFunctions
    {
        public string GetStringWithParameters(int i, string s) => "";
        public int GetIntNoParameters() => 0;
        public int GetIntWithParameters(int i, string s) => 0;
    }
}