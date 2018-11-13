using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FbonizziMonoGame.Drawing
{
    /// <summary>
    /// Generates a view matrix that represents the transformation needed
    /// to scale a virtual rectangle to fit the real graphics device viewport.
    /// For example: your game view screen is a 800x600 rectangle but you need to fit 
    /// it into a 1920x1080 device screen.
    /// </summary>
    public class ScalingMatrixProvider : IScreenTransformationMatrixProvider
    {
        /// <summary>
        /// The device screen width in which the <see cref="VirtualWidth"/> has to fit 
        /// </summary>
        public int RealScreenWidth { get; protected set; }

        /// <summary>
        /// The device screen height in which the <see cref="VirtualHeight"/> has to fit 
        /// </summary>
        public int RealScreenHeight { get; protected set; }

        /// <summary>
        /// The virtual width that should fit to the <see cref="RealScreenWidth"/>
        /// </summary>
        public int VirtualWidth { get; }

        /// <summary>
        /// The virtual height that should fit to the <see cref="RealScreenHeight"/>
        /// </summary>
        public int VirtualHeight { get; }

        /// <summary>
        /// The scale matrix that defines the transformation needed to fit <see cref="VirtualWidth"/> and <see cref="VirtualHeight"/>
        /// into <see cref="RealScreenWidth"/> and <see cref="RealScreenHeight"/>
        /// </summary>
        public Matrix ScaleMatrix { get; protected set; }

        /// <summary>
        /// Scaling matrix provider constructor
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="virtualWidth"></param>
        /// <param name="virtualHeight"></param>
        public ScalingMatrixProvider(
            GraphicsDevice graphicsDevice,
            int virtualWidth,
            int virtualHeight)
        {
            if (graphicsDevice == null)
                throw new ArgumentNullException(nameof(graphicsDevice));

            RealScreenWidth = graphicsDevice.Viewport.Width;
            RealScreenHeight = graphicsDevice.Viewport.Height;
         
            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;

            var scaleX = (float)RealScreenWidth / virtualWidth;
            var scaleY = (float)RealScreenHeight / virtualHeight;
            ScaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }
    }
}
