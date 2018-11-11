using System;

namespace FbonizziMonoGame
{
    public interface IFbonizziGame
    {  
        void Pause();
        void Resume();
        event EventHandler ExitGameRequested;
    }
}
