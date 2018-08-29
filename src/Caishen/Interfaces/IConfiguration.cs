using System.Collections.Generic;

namespace Caishen.Interfaces
{
    public interface IConfiguration
    {
        ICacheConfiguration DefaultCacheConfiguration { get; }
        IEnumerable<ISingleConfiguration> MethodsConfigurations { get; }
    }
}