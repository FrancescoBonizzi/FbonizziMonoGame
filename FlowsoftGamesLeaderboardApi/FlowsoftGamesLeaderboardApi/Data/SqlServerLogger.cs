using Dapper;
using FlowsoftGamesLeaderboardApi.Data.Infrastructure;
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApi.Data
{
    public class SqlServerLogger : ILogger
    {
        private readonly string _leaderboardConnectionString;

        public SqlServerLogger(string leaderboardConnectionString)
        {
            if (string.IsNullOrWhiteSpace(leaderboardConnectionString))
            {
                throw new ArgumentNullException(nameof(leaderboardConnectionString));
            }

            _leaderboardConnectionString = leaderboardConnectionString;
        }

        public async Task LogErrorAsync(string gameName, string message, Exception exception, [CallerMemberName] string methodName = null)
        {
            using (var connection = new SqlConnection(_leaderboardConnectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(
                    "Leaderboard.proc_ErrorLogs_Insert",
                    new
                    {
                        @GameName = gameName,
                        @MethodName = methodName,
                        @Message = $"{message}{Environment.NewLine}{Environment.NewLine}{exception.ToString()}"
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
                connection.Close();
            }
        }
    }
}
