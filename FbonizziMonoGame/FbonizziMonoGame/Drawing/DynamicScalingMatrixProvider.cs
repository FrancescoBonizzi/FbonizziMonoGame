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
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly GameWindow _window;

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
        /// <param name="graphicsDeviceManager"></param>
        /// <param name="virtualWidth"></param>
        /// <param name="virtualHeight"></param>
        public DynamicScalingMatrixProvider(
            GameWindow window, GraphicsDeviceManager graphicsDeviceManager, 
            int virtualWidth, int virtualHeight)
        {
            _graphicsDeviceManager = graphicsDeviceManager ?? throw new ArgumentNullException(nameof(graphicsDeviceManager));
            _window = window ?? throw new ArgumentNullException(nameof(window));
            _graphicsDevice = graphicsDeviceManager.GraphicsDevice;

            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;

            window.ClientSizeChanged += OnClientSizeChanged;
            OnClientSizeChanged(null, EventArgs.Empty);
        }

        private void OnClientSizeChanged(object sender, EventArgs eventArgs)
        {
            var viewport = _graphicsDevice.Viewport;
            RealScreenWidth = viewport.Width;
            RealScreenHeight = viewport.Height;

            var worldScaleX = (float)viewport.Width / VirtualWidth;
            var worldScaleY = (float)viewport.Height / VirtualHeight;

            var scale = MathHelper.Max(worldScaleX, worldScaleY);

            var width = (int)(scale * VirtualWidth + 0.5f);
            var height = (int)(scale * VirtualHeight + 0.5f);

            var x = viewport.Width / 2 - width / 2;
            var y = viewport.Height / 2 - height / 2;
            _graphicsDevice.Viewport = new Viewport(x, y, width, height);

            if ((_graphicsDeviceManager != null) &&
                ((_graphicsDeviceManager.PreferredBackBufferWidth != _window.ClientBounds.Width) ||
                 (_graphicsDeviceManager.PreferredBackBufferHeight != _window.ClientBounds.Height)))
            {
                _graphicsDeviceManager.PreferredBackBufferWidth = _window.ClientBounds.Width;
                _graphicsDeviceManager.PreferredBackBufferHeight = _window.ClientBounds.Height;
                _graphicsDeviceManager.ApplyChanges();
            }

            var scaleY = (float)_graphicsDevice.Viewport.Height / VirtualHeight;
            var scaleX = (float)_graphicsDevice.Viewport.Width / VirtualWidth;
            ScaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            ScaleMatrixChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
