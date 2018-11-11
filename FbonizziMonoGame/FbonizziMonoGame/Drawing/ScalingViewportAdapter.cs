using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FbonizziMonoGame.Drawing.ViewportAdapters
{
    public class ScalingViewportAdapter
    {
        public GraphicsDevice GraphicsDevice { get; }
        public Viewport Viewport { get; }

        public int RealScreenWidth { get; protected set; }
        public int RealScreenHeight { get; protected set; }

        public int VirtualWidth { get; }
        public int VirtualHeight { get; }

        public Rectangle VirtualBoundingRectangle { get; }

        public Matrix ScaleMatrix { get; protected set; }

        public ScalingViewportAdapter(
            GraphicsDevice graphicsDevice,
            int virtualWidth,
            int virtualHeight)
        {
            GraphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            RealScreenWidth = GraphicsDevice.Viewport.Width;
            RealScreenHeight = GraphicsDevice.Viewport.Height;
            Viewport = GraphicsDevice.Viewport;

            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;

            VirtualBoundingRectangle = new Rectangle(0, 0, virtualWidth, virtualHeight);

            var scaleX = (float)RealScreenWidth / virtualWidth;
            var scaleY = (float)RealScreenHeight / virtualHeight;
            ScaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }

        public Point PointToScreen(Point point)
            => PointToScreen(point.X, point.Y);

        public virtual Point PointToScreen(int x, int y)
        {
            var invertedMatrix = Matrix.Invert(ScaleMatrix);
            return Vector2.Transform(new Vector2(x, y), invertedMatrix).ToPoint();
        }
    }
}
