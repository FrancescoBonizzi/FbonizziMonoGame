using FbonizziMonoGame.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FbonizziMonoGame.Extensions
{
    public static class SpriteBatchExtensions
    {
        private static readonly Color _hitBoxColor = Color.Red.WithAlpha(0.5f);
        private static Texture2D _rectangleTexture;

        public static void DrawRectangle(
           this SpriteBatch spriteBatch,
           Rectangle rectangleDefinition,
           Color rectangleColor)
        {
            if (_rectangleTexture == null)
            {
                _rectangleTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _rectangleTexture.SetData(new Color[] { Color.White });
            }

            spriteBatch.Draw(
                _rectangleTexture,
                rectangleDefinition,
                rectangleColor);
        }

        public static void Draw(
            this SpriteBatch spriteBatch,
            Sprite sprite,
            DrawingInfos spatialObject,
            bool isDebugModeEnabled = false)
        {
            spriteBatch.Draw(
                sprite.Sheet,
                spatialObject.Position,
                sprite.SourceRectangle,
                spatialObject.OverlayColor,
                spatialObject.Rotation,
                spatialObject.Origin,
                spatialObject.Scale,
                spatialObject.IsFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f);

            if (isDebugModeEnabled)
            {
                DrawRectangle(
                    spriteBatch,
                    spatialObject.HitBox(sprite.Width, sprite.Height),
                    _hitBoxColor);
            }
        }

        public static void Draw(
            this SpriteBatch spriteBatch,
            Sprite sprite,
            Color? color = null)
        {
            spriteBatch.Draw(
                sprite.Sheet,
                Vector2.Zero,
                sprite.SourceRectangle,
                color ?? Color.White);
        }

        public static void DrawString(
            this SpriteBatch spriteBatch,
            SpriteFont font,
            string text,
            DrawingInfos spatialInfos)
        {
            spriteBatch.DrawString(
                font,
                text,
                spatialInfos.Position,
                spatialInfos.OverlayColor,
                spatialInfos.Rotation,
                spatialInfos.Origin,
                spatialInfos.Scale,
                spatialInfos.IsFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f);
        }
    }
}
