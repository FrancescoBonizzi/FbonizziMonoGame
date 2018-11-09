using FlowsoftGamesLeaderboardApi.Data.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApi.Data
{
    public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _cache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public Task<string> Get(string key)
        {
            return Task.FromResult(_cache.Get<string>(key));
        }

        public Task Remove(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task Set(string key, string value, TimeSpan expiration)
        {
            _cache.Set(key, value, expiration);
            return Task.CompletedTask;
        }
    }
}
