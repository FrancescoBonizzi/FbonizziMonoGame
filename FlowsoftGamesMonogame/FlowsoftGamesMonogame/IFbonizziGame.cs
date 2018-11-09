using System;

namespace FbonizziGames
{
    public interface IFbonizziGame
    {
        void Pause();
        void Resume();
        event EventHandler ExitGameRequested;
    }
}
