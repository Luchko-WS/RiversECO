using System;
using System.Threading.Tasks;

namespace RiversECO.Cache.Contracts
{
    public interface IAppMemoryCache
    {
        TValue GetOrCreate<TValue>(string key, Func<TValue> cachedValueFactory);
        Task<TValue> GetOrCreateAsync<TValue>(string key, Func<Task<TValue>> cachedValueFactory);
        void Remove(string key);
    }
}