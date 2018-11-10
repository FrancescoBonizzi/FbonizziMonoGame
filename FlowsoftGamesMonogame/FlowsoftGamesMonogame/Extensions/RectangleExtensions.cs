using Microsoft.Xna.Framework;

namespace FbonizziMonogame.Extensions
{
    public static class RectangleExtensions
    {
        public static Rectangle WithTolerance(
            this Rectangle me,
            Rectangle tolerance)
        {
            return new Rectangle(
                me.X + tolerance.X,
                me.Y + tolerance.Y,
                me.Width - tolerance.Width,
                me.Height - tolerance.Height);
        }

        public static Rectangle WithTolerance(
            this Rectangle me,
            int commonTolerance)
        {
            var tolerance = new Rectangle(commonTolerance, commonTolerance, commonTolerance, commonTolerance);

            return me.WithTolerance(tolerance);
        }
    }
}
