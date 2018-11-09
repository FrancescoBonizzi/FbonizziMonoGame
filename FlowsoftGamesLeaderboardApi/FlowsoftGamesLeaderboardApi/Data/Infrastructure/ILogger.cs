using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApi.Data.Infrastructure
{
    public interface ILogger
    {
        Task LogErrorAsync(string gameName, string message, Exception exception, [CallerMemberName]string methodName = null);
    }
}
