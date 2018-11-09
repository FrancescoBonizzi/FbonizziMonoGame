using FbonizziGames.Drawing;
using FbonizziGames.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FbonizziGames.Achievements
{
    public class AchievementToDraw
    {
        private readonly SpriteFont _font;
        public AchievementItem AchievementItem { get; }
        private readonly Sprite _symbolSprite;
        private readonly Vector2 _spriteStartingPosition;
        private readonly DrawingInfos _spriteDrawingInfos;
        private readonly Vector2 _titleStartingPosition;
        private readonly DrawingInfos _titleDrawingInfos;
        private readonly Vector2 _textStartingPosition;
        private readonly DrawingInfos _textDrawingInfos;
        private readonly Vector2 _completeDateStartingPosition;
        private readonly DrawingInfos _completedDateDrawingInfos;

        public const int Width = 380;
        public const int Height = 40;

        private Rectangle _container = new Rectangle(
            0, 0,
            Width, Height);
        public Rectangle Container => _container;

        private readonly Color _startingRectangleColor = Color.DarkGray;
        private Color _rectangleColor;

        public AchievementToDraw(
            AchievementItem achievementItem,
            SpriteFont font, float fontSize,
            Sprite symbol)
        {
            _font = font;
            AchievementItem = achievementItem;
            _symbolSprite = symbol;

            float alpha = 1f;
            _rectangleColor = _startingRectangleColor.WithAlpha(0.8f);

            if (achievementItem.CompletedDate == default(DateTime))
            {
                alpha = 0.3f;
                _rectangleColor = _startingRectangleColor.WithAlpha(0.3f);
            }
            
            _spriteStartingPosition = new Vector2(30f, Container.Height / 2);
            _spriteDrawingInfos = new DrawingInfos()
            {
                Position = _spriteStartingPosition,
                Origin = symbol.SpriteCenter,
                OverlayColor = Color.White.WithAlpha(alpha)
            };

            _titleStartingPosition = new Vector2(60f, 2f);
            _titleDrawingInfos = new DrawingInfos()
            {
                Position = _titleStartingPosition,
                Scale = fontSize,
                OverlayColor = Color.White.WithAlpha(alpha)
            };

            _textStartingPosition = new Vector2(60f, Container.Height - 15f);
            _textDrawingInfos = new DrawingInfos()
            {
                Position = _textStartingPosition,
                Scale = fontSize / 1.4f,
                OverlayColor = Color.White.WithAlpha(alpha)
            };

            _completeDateStartingPosition = new Vector2(Container.Width - 60f, Container.Height / 4);
            _completedDateDrawingInfos = new DrawingInfos()
            {
                Position = _completeDateStartingPosition,
                Scale = fontSize / 1.4f,
                Origin = font.GetTextCenter(AchievementItem.CompletedDateString),
                OverlayColor = Color.White.WithAlpha(alpha)
            };
        }

        public void SetCompletionDate(DateTime completionDate)
        {
            AchievementItem.CompletedDate = completionDate;
            _completedDateDrawingInfos.Origin = _font.GetTextCenter(AchievementItem.CompletedDateString);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2? position = null)
        {
            if (position != null)
            {
                SetPosition(position.Value);
            }

            spriteBatch.DrawRectangle(Container, _rectangleColor);
            spriteBatch.DrawString(_font, AchievementItem.Title, _titleDrawingInfos);
            spriteBatch.Draw(_symbolSprite, _spriteDrawingInfos);
            spriteBatch.DrawString(_font, AchievementItem.Text, _textDrawingInfos);

            if (AchievementItem.CompletedDate != default(DateTime))
                spriteBatch.DrawString(_font, AchievementItem.CompletedDateString, _completedDateDrawingInfos);
        }

        public void SetPosition(Vector2 position)
        {
            // In somma perché deve essere tutto relativo,
            // tutte le parti dell'oggetto devono muoversi insieme
            _completedDateDrawingInfos.Position = _completeDateStartingPosition + position;
            _spriteDrawingInfos.Position = _spriteStartingPosition + position;
            _textDrawingInfos.Position = _textStartingPosition + position;
            _titleDrawingInfos.Position = _titleStartingPosition + position;
            _container.X = (int)position.X;
            _container.Y = (int)position.Y;
        }
        
        public void SetAlpha(float alpha)
        {
            _rectangleColor = _startingRectangleColor.WithAlpha(alpha);
            _completedDateDrawingInfos.OverlayColor = Color.White.WithAlpha(alpha);
            _spriteDrawingInfos.OverlayColor = Color.White.WithAlpha(alpha);
            _textDrawingInfos.OverlayColor = Color.White.WithAlpha(alpha);
            _titleDrawingInfos.OverlayColor = Color.White.WithAlpha(alpha);
        }
    }

}
