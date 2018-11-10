using System;

namespace FbonizziMonogame
{
    public interface IFbonizziGame
    {
        void Pause();
        void Resume();
        event EventHandler ExitGameRequested;
    }
}
