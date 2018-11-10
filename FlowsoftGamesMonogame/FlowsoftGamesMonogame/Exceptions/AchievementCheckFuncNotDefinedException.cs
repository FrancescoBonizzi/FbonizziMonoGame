using System;

namespace FbonizziMonogame.Exceptions
{
    public class AchievementCheckFuncNotDefinedException : Exception
    {
        public AchievementCheckFuncNotDefinedException(string achievementName)
            : base ($"Completion check func not set for achievement: {achievementName}") { }
    }
}
