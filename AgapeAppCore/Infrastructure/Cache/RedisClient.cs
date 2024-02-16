using StackExchange.Redis;
using System.Text.Json;
using System;
using System.Threading.Tasks;


namespace AgapeAppCore.Infrastructure.Cache
{
    public class RedisClient(string connectionString) : ICacheManager
    {
        private readonly Lazy<ConnectionMultiplexer> lazyConnection = new(() => ConnectionMultiplexer.Connect(connectionString));

        private IDatabase Database
        {
            get
            {
                return lazyConnection.Value.GetDatabase();
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await Database.StringSetAsync(key, serializedValue, expiry);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var serializedValue = await Database.StringGetAsync(key);
            return serializedValue.HasValue ? JsonSerializer.Deserialize<T>(serializedValue) : default(T);
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> fetchFunction, TimeSpan? expiry = null)
        {
            var serializedValue = await Database.StringGetAsync(key);

            if (serializedValue.HasValue)
            {
                return JsonSerializer.Deserialize<T>(serializedValue);
            }

            var result = await fetchFunction();

            if (result is not null)
            {
                await SetAsync(key, result, expiry);
            }

            return result;
        }

        public void Remove(string keyName)
        {
            Database.KeyDelete(keyName);
        }
    }
}