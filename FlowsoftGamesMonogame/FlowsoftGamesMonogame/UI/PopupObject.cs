using FbonizziGames.Extensions;
using Microsoft.Xna.Framework;
using System;

namespace FbonizziGames.UI
{
    public class PopupObject
    {
        private readonly TimeSpan _lifeTime;
        private Color _originalColor;
        private readonly float _upSpeed;
        private TimeSpan _elapsed;

        public Color OverlayColor { get; set; }
        public float Alpha { get; private set; }
        public Vector2 Position { get; set; }

        private readonly Vector2 _startingPosition;

        public event EventHandler Completed;
        private FadeState _currentFadeState;

        public PopupObject(
            TimeSpan lifeTime,
            Vector2 startingPosition,
            Color originalColor,
            float upSpeed = 5f)
        {
            Position = startingPosition;
            _startingPosition = startingPosition;
            _lifeTime = lifeTime;
            _originalColor = originalColor;
            _upSpeed = upSpeed;
            _currentFadeState = FadeState.Static;
        }

        public bool IsCompleted =>
            _currentFadeState == FadeState.Static;

        enum FadeState
        {
            Static,
            PoppingUp
        }

        public void Popup()
        {
            _elapsed = TimeSpan.Zero;
            Alpha = 0f;
            OverlayColor = _originalColor.WithAlpha(Alpha);
            _currentFadeState = FadeState.PoppingUp;
        }

        public void Update(TimeSpan elapsed)
        {
            switch (_currentFadeState)
            {
                case FadeState.PoppingUp:

                    _elapsed += elapsed;

                    if (_elapsed >= _lifeTime)
                    {
                        _currentFadeState = FadeState.Static;
                        Alpha = 0f;
                        OverlayColor = _originalColor.WithAlpha(Alpha);
                        Completed?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        float normalizedLifetime = (float)_elapsed.Ticks / _lifeTime.Ticks;
                        Alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
                        OverlayColor = _originalColor.WithAlpha(Alpha);
                        Position = new Vector2(_startingPosition.X, _startingPosition.Y - normalizedLifetime * _upSpeed);
                    }

                    break;
            }
        }
    }
}
