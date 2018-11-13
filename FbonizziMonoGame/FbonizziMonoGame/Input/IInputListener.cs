using Microsoft.Xna.Framework;

namespace FbonizziMonoGame.Input
{
    /// <summary>
    /// An listener that pools for input
    /// </summary>
    public interface IInputListener
    {
        /// <summary>
        /// Manages the pooling logic
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);
    }
}
