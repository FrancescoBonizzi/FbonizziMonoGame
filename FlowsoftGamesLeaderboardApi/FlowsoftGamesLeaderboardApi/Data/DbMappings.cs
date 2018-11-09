using FlowsoftGamesLeaderboardApi.Exceptions;

namespace FlowsoftGamesLeaderboardApi.Data
{
    public static class DbMappings
    {
        public static byte GetGameId(string gameName)
        {
            switch(gameName)
            {
                case "Rellow":
                    return 1;
            }

            throw new InvalidGameException($"Game: {gameName} is not present in DB");
        }

        public static byte GetScoreTypeId(string scoreTypeName)
        {
            switch (scoreTypeName)
            {
                case "Score":
                    return 1;
            }

            throw new InvalidGameException($"ScoreType: {scoreTypeName} is not present in DB");
        }
    }
}
