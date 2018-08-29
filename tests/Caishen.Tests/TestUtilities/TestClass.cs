namespace Caishen.Tests.TestUtilities
{
    public interface ITestClass
    {
        int Two { get; }
        int GetNextInt(int value);
    }

    public class TestClass : ITestClass
    {
        public int Two { get; } = 2;
        public int GetNextInt(int value) => value + 1;
    }
}