using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApi.Data.Infrastructure
{
    public interface ILeaderboardRepository
    {
        Task<int> GetOrSetGlobalRank(string userId, string gameName, string scoreTypeName, long currentLocalScore);
    }
}
