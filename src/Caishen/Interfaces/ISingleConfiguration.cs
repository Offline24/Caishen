namespace Caishen.Interfaces
{
    public interface ISingleConfiguration
    {
        ICacheConfiguration CacheConfiguration { get; }
        IMethodCheckerConfiguration MethodCheckerConfiguration { get; }
    }
}