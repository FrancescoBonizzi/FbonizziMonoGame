using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;

namespace FbonizziMonoGameGallery.ParticleGeneratorTimedEffect
{
    public class ParticleGeneratorTimedEffectGame : WpfGame
    {
        private IGraphicsDeviceService _graphicsDeviceManager;

        protected override void Initialize()
        {
            // must be initialized. required by Content loading and rendering (will add itself to the Services)
            // note that MonoGame requires this to be initialized in the constructor, while WpfInterop requires it to
            // be called inside Initialize (before base.Initialize())
            _graphicsDeviceManager = new WpfGraphicsDeviceService(this);

            // must be called after the WpfGraphicsDeviceService instance was created
            base.Initialize();

            // content loading now possible
        }

        protected override void Update(GameTime time)
        {
            // TODO
        }

        protected override void Draw(GameTime time)
        {
            // TODO
        }
    }
}
