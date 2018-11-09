using FbonizziGames.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FbonizziGames.UI
{
    public class ButtonWithText
    {
        private readonly SpriteFont _font;
        private readonly string _text;

        private readonly Rectangle _collisionRectangle;
        private readonly Rectangle _shadowRectangle;
        private readonly Vector2 _origin;
        private readonly Vector2 _textPosition;

        private readonly Color _backgroundColor;
        private readonly Color _shadowColor;
        private readonly Color _textColor;

        private readonly Action _onClick;

        public float TextScale { get; set; }

        public ButtonWithText(
            SpriteFont font,
            string text,
            Rectangle collisionRectangle,
            Color backgroundColor,
            Color textColor, 
            Color shadowColor,
            Action onClick,
            float textPadding)
        {
            _font = font ?? throw new ArgumentNullException(nameof(font));
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _collisionRectangle = collisionRectangle;
            _backgroundColor = backgroundColor;
            _textColor = textColor;
            _shadowColor = shadowColor;
            _onClick = onClick ?? throw new ArgumentNullException(nameof(onClick));

            _shadowRectangle = new Rectangle(
                _collisionRectangle.X + 4,
                _collisionRectangle.Y + 4,
                _collisionRectangle.Width,
                _collisionRectangle.Height);

            var textSize = _font.MeasureString(text);
            _origin = textSize / 2f;
            
            _textPosition = new Vector2(
                _collisionRectangle.X + _collisionRectangle.Width / 2f,
                _collisionRectangle.Y + _collisionRectangle.Height / 2f);

            float expectedButtonTextWidth = collisionRectangle.Width - textPadding * 2f;
            TextScale = expectedButtonTextWidth / textSize.X;
        }

        public void HandleInput(Vector2 inputPosition)
        {
            if (_collisionRectangle.Contains(inputPosition))
            {
                _onClick();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(_shadowRectangle, _shadowColor);
            spriteBatch.DrawRectangle(_collisionRectangle, _backgroundColor);
            spriteBatch.DrawString(_font, _text, _textPosition, _textColor, 0f, _origin, TextScale, SpriteEffects.None, 0f);
        }
    }
}
