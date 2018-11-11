using System;

namespace FbonizziMonoGame.Exceptions
{
    public class AchievementCheckFuncNotDefinedException : Exception
    {
        public AchievementCheckFuncNotDefinedException(string achievementName)
            : base ($"Completion check func not set for achievement: {achievementName}") { }
    }
}
