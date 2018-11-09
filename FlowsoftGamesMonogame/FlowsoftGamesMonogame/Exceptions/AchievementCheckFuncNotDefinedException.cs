using System;

namespace FbonizziGames.Exceptions
{
    public class AchievementCheckFuncNotDefinedException : Exception
    {
        public AchievementCheckFuncNotDefinedException(string achievementName)
            : base ($"Completion check func not set for achievement: {achievementName}") { }
    }
}
