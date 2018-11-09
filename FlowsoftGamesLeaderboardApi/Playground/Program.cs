using FlowsoftGamesLeaderboardApiSDK;
using System;
using System.Threading.Tasks;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var endpoint = args[0];
                var userId = args[1];
                var game = args[2];
                var scoreType = args[3];
                var score = args[4];

                var leaderboardGateway = new HttpLeaderboard(endpoint, game);

                var globalRank2 = leaderboardGateway.GetGlobalRank(userId, scoreType, Convert.ToInt64(score)).GetAwaiter().GetResult();
                var globalRank1 = leaderboardGateway.GetGlobalRank($"{userId}-Better", scoreType, Convert.ToInt64(score) + 1).GetAwaiter().GetResult();
                Console.WriteLine($"getGlobalRankResponse 1: {globalRank1}");
                Console.WriteLine($"getGlobalRankResponse 2: {globalRank2}");

                Task.Delay(6000).GetAwaiter().GetResult();

                globalRank2 = leaderboardGateway.GetGlobalRank(userId, scoreType, Convert.ToInt64(score)).GetAwaiter().GetResult();
                globalRank1 = leaderboardGateway.GetGlobalRank($"{userId}-Better", scoreType, Convert.ToInt64(score) + 1).GetAwaiter().GetResult();
                Console.WriteLine($"getGlobalRankResponse 1: {globalRank1}");
                Console.WriteLine($"getGlobalRankResponse 2: {globalRank2}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Read();
        }
    }
}
