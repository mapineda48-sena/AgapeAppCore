
using System;
using System.Threading.Tasks;

namespace AgapeAppCore.Infrastructure.Cache
{
    public interface ICacheManager
    {
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);

        Task<T> GetAsync<T>(string key);

        Task<T> GetAsync<T>(string key, Func<Task<T>> fetchFunction, TimeSpan? expiry = null);

        void Remove(string keyName);
    }
}