using Dapper;
using FlowsoftGamesLeaderboardApi.Data.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApi.Data
{
    public class SqlServerLeaderboardRepository : ILeaderboardRepository
    {
        private readonly string _leaderboardConnectionString;

        public SqlServerLeaderboardRepository(string leaderboardConnectionString)
        {
            if (string.IsNullOrWhiteSpace(leaderboardConnectionString))
            {
                throw new ArgumentNullException(nameof(leaderboardConnectionString));
            }

            _leaderboardConnectionString = leaderboardConnectionString;
        }

        private async Task<long?> GetScore(IDbConnection connection, string userId, byte gameId, short scoreTypeId)
        {
            return await connection.ExecuteScalarAsync<long?>(
                "Leaderboard.proc_GetScore",
                new
                {
                    @UserId = userId,
                    @GameId = gameId,
                    @ScoreTypeId = scoreTypeId
                },
                commandType: CommandType.StoredProcedure);
        }

        private async Task CreateScore(IDbConnection connection, string userId, byte gameId, short scoreTypeId, long score)
        {
            await connection.ExecuteScalarAsync<long?>(
                "Leaderboard.proc_CreateScore",
                new
                {
                    @UserId = userId,
                    @GameId = gameId,
                    @ScoreTypeId = scoreTypeId,
                    @Score = score
                },
                commandType: CommandType.StoredProcedure);
        }

        private async Task SetScore(IDbConnection connection, string userId, byte gameId, short scoreTypeId, long score)
        {
            await connection.ExecuteScalarAsync<long?>(
                "Leaderboard.proc_SetScore",
                new
                {
                    @UserId = userId,
                    @GameId = gameId,
                    @ScoreTypeId = scoreTypeId,
                    @Score = score
                },
                commandType: CommandType.StoredProcedure);
        }

        private async Task<int> GetGlobalRank(IDbConnection connection, string userId, byte gameId, short scoreTypeId)
        {
            return await connection.ExecuteScalarAsync<int>(
                "Leaderboard.proc_GetGlobalRank",
                new
                {
                    @UserId = userId,
                    @GameId = gameId,
                    @ScoreTypeId = scoreTypeId
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetOrSetGlobalRank(string userId, string gameName, string scoreTypeName, long currentLocalScore)
        {
            var gameId = DbMappings.GetGameId(gameName);
            var scoreTypeId = DbMappings.GetScoreTypeId(scoreTypeName);

            using (var connection = new SqlConnection(_leaderboardConnectionString))
            {
                var savedScore = await GetScore(connection, userId, gameId, scoreTypeId);
                if (savedScore == null)
                {
                    await CreateScore(connection, userId, gameId, scoreTypeId, currentLocalScore);
                }
                else if (currentLocalScore > savedScore)
                {
                    await SetScore(connection, userId, gameId, scoreTypeId, currentLocalScore);
                }

                var globalRank = await GetGlobalRank(connection, userId, gameId, scoreTypeId);
                connection.Close();

                return globalRank;
            }
        }

    }
}
