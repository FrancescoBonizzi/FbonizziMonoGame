using Microsoft.Xna.Framework;

namespace FbonizziMonogame.Drawing
{
    public class DrawingInfos
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public bool IsFlipped { get; set; } = false;
        public Color OverlayColor { get; set; } = Color.White;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public float Scale { get; set; } = 1.0f;
        public float Rotation { get; set; } = 0.0f;
        public Rectangle HitBoxTolerance { get; set; } = Rectangle.Empty;

        public Rectangle HitBox(int width, int height) =>
            new Rectangle(
                (int)(Position.X - Origin.X) + HitBoxTolerance.X,
                (int)(Position.Y - Origin.Y) + HitBoxTolerance.Y,
                (width - HitBoxTolerance.Width),
                (height - HitBoxTolerance.Height));

        public Vector2 Center(int width, int height)
        {
            var originCalculatedPosition = Position - Origin;
            return new Vector2(
                originCalculatedPosition.X + width / 2,
                originCalculatedPosition.Y + height / 2);
        }
    }
}