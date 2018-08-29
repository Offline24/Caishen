namespace Caishen
{
    public interface ICacheFactory<TInterface> where TInterface : class
    {        
        TInterface Create(TInterface underlyingObject);
    }
}