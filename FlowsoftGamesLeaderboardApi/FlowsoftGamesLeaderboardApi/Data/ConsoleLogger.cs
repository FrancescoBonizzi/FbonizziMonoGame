using FlowsoftGamesLeaderboardApi.Data.Infrastructure;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApi.Data
{
    public class ConsoleLogger : ILogger
    {
        public Task LogErrorAsync(string applicationName, string message, Exception exception, [CallerMemberName] string methodName = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error! {methodName}: {message} - {exception.Message}");
            Console.ForegroundColor = ConsoleColor.White;

            return Task.CompletedTask;
        }
    }
}
