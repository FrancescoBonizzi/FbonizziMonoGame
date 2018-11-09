using System;

namespace FlowsoftGamesLeaderboardApi.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException(string message) : base(message) { }
    }
}
