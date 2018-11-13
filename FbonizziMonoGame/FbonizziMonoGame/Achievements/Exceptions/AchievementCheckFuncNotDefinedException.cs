using System;

namespace FbonizziMonoGame.Achievements.Exceptions
{
    public class AchievementCheckFuncNotDefinedException : Exception
    {
        public AchievementCheckFuncNotDefinedException(string achievementName)
            : base ($"Completion check func not set for achievement: {achievementName}") { }
    }
}
