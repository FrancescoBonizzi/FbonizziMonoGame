using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace FbonizziMonoGame.Extensions
{
    public static class SpriteFontExtensions
    {
        public static Vector2 GetTextCenter(
            this SpriteFont font,
            StringBuilder text)
        {
            var renderedTextSize = font.MeasureString(text);
            return new Vector2(renderedTextSize.X / 2f, renderedTextSize.Y / 2f);
        }

        public static Vector2 GetTextCenter(
            this SpriteFont font,
            string text)
        {
            var renderedTextSize = font.MeasureString(text);
            return new Vector2(renderedTextSize.X / 2f, renderedTextSize.Y / 2f);
        }

    }
}
