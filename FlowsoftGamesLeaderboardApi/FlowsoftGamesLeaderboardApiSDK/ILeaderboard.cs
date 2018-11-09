using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApiSDK
{
    public interface ILeaderboard
    {
        Task<int> GetGlobalRank(string userId, string scoreType, long currentLocalScore);
    }
}