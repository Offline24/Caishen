namespace Caishen.Tests.TestUtilities
{
    public interface ITestClassWithDependency
    {
        int GetNextInt(int value);
    }

    public class TestClassWithDependency : ITestClassWithDependency
    {
        private readonly Dependency _dependecy;

        private TestClassWithDependency(Dependency dependecy)
        {
            _dependecy = dependecy;
        }

        public static TestClassWithDependency Create()
        {
            var dependency = new Dependency(isOpen: false);
            dependency.Open();
            
            return new TestClassWithDependency(dependency);
        }

        public int GetNextInt(int value)
        {
            return _dependecy.GetNextIntIfOpen(value);
        }
        
        private class Dependency
        {
            private bool _isOpen;

            public Dependency(bool isOpen)
            {
                _isOpen = isOpen;
            }

            public void Open()
            {
                _isOpen = true;
            }

            public int GetNextIntIfOpen(int value)
            {
                if (_isOpen)
                {
                    return value + 1;
                }

                return 0;
            }
        }
    }
}