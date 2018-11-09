using System;

namespace FlowsoftGamesLeaderboardApi.Exceptions
{
    public class InvalidGameException : Exception
    {
        public InvalidGameException(string message) 
            : base(message) { }
    }
}
