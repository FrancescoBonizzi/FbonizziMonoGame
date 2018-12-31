using FbonizziMonoGame.Drawing.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FbonizziMonoGame.Drawing
{
    /// <summary>
    /// Generates a scaling view matrix that changes when the device screen is resized
    /// </summary>
    public class DynamicScalingMatrixProvider : IScreenTransformationMatrixProvider
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly IScreenSizeChangedNotifier _window;

        /// <summary>
        /// The device screen width in which the <see cref="VirtualWidth"/> has to fit 
        /// </summary>
        public int RealScreenWidth { get; private set; }

        /// <summary>
        /// The device screen height in which the <see cref="VirtualHeight"/> has to fit 
        /// </summary>
        public int RealScreenHeight { get; private set; }

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
        public Matrix ScaleMatrix { get; private set; }

        /// <summary>
        /// Virtual bounding rectangle
        /// </summary>
        public Rectangle VirtualBoundingRectangle => new Rectangle(0, 0, VirtualWidth, VirtualHeight);

        /// <summary>
        /// Raises an event when the scale matrix changed (in relation to device screen size change)
        /// </summary>
        public event EventHandler ScaleMatrixChanged;

        /// <summary>
        /// Projects the given coordinates into screen coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Point PointToScreen(int x, int y)
        {
            var invertedMatrix = Matrix.Invert(ScaleMatrix);
            return Vector2.Transform(new Vector2(x, y), invertedMatrix).ToPoint();
        }

        /// <summary>
        /// Dynamic scaling matrix provider constructor
        /// </summary>
        /// <param name="window"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="virtualWidth"></param>
        /// <param name="virtualHeight"></param>
        public DynamicScalingMatrixProvider(
            IScreenSizeChangedNotifier window,
            GraphicsDevice graphicsDevice,
            int virtualWidth, int virtualHeight)
        {
            _window = window ?? throw new ArgumentNullException(nameof(window));
            _graphicsDevice = graphicsDevice;

            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;

            window.OnScreenSizeChanged += OnScreenSizeChanged;
            OnScreenSizeChanged(null, EventArgs.Empty);
        }

        private void OnScreenSizeChanged(object sender, EventArgs eventArgs)
        {
            var viewport = _graphicsDevice.Viewport;
            RealScreenWidth = viewport.Width;
            RealScreenHeight = viewport.Height;

            var worldScaleX = (float)viewport.Width / VirtualWidth;
            var worldScaleY = (float)viewport.Height / VirtualHeight;
            var scale = MathHelper.Min(worldScaleX, worldScaleY);

            ScaleMatrix = Matrix.CreateScale(scale, scale, 1.0f);
            ScaleMatrixChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
