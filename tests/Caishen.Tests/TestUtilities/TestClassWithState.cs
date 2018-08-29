namespace Caishen.Tests.TestUtilities
{
    public interface ITestClassWithState
    {
        int GetNextInt();
    }

    public class TestClassWithState : ITestClassWithState
    {
        public int InternalState { get; private set; }

        public int GetNextInt() => InternalState++;
    }
}