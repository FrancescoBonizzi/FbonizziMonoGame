using System;
using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApi.Data.Infrastructure
{
    public interface ICache
    {
        Task Set(string key, string value, TimeSpan expiration);
        Task<string> Get(string key);
        Task Remove(string key);
    }
}
