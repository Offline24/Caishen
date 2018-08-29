namespace Caishen.Interfaces
{
    public interface ICache
    {
        object GetFromCache(string key);
        void UpdateCache(string key, object value);
    }
}