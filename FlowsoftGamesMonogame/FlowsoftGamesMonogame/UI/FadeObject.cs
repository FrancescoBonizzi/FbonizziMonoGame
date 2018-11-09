using FbonizziGames.Extensions;
using Microsoft.Xna.Framework;
using System;

namespace FbonizziGames.UI
{
    public class FadeObject
    {
        private Color _originalColor;
        private readonly TimeSpan _fadeDuration;
        private TimeSpan _elapsed;

        public event EventHandler FadeOutCompleted;
        public event EventHandler FadeInCompleted;

        private FadeState _currentFadeState;

        public Color OverlayColor { get; set; }
        public bool IsFading
            => _currentFadeState != FadeState.Static;

        public float CurrentAlpha { get; private set; }

        public FadeObject(
            TimeSpan fadeDuration,
            Color originalColor)
        {
            _fadeDuration = fadeDuration;
            _originalColor = originalColor;
            _currentFadeState = FadeState.Static;
            CurrentAlpha = 1f;
        }

        enum FadeState
        {
            Static,
            FadeIn,
            FadeOut
        }

        public void FadeIn()
        {
            _currentFadeState = FadeState.FadeIn;
            CurrentAlpha = 0f;
            OverlayColor = _originalColor.WithAlpha(CurrentAlpha);
            _elapsed = TimeSpan.Zero;
        }

        public void FadeOut()
        {
            _currentFadeState = FadeState.FadeOut;
            CurrentAlpha = 1f;
            OverlayColor = _originalColor.WithAlpha(CurrentAlpha);
            _elapsed = TimeSpan.Zero;
        }

        public void Update(TimeSpan elapsed)
        {
            switch (_currentFadeState)
            {
                case FadeState.FadeIn:

                    _elapsed += elapsed;

                    if (_elapsed >= _fadeDuration)
                    {
                        _currentFadeState = FadeState.Static;
                        CurrentAlpha = 1f;
                        OverlayColor = _originalColor.WithAlpha(CurrentAlpha);
                        FadeInCompleted?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        CurrentAlpha = (float)(_elapsed.TotalSeconds / _fadeDuration.TotalSeconds);
                        OverlayColor = _originalColor.WithAlpha(CurrentAlpha);
                    }

                    break;

                case FadeState.FadeOut:

                    _elapsed += elapsed;

                    if (_elapsed >= _fadeDuration)
                    {
                        _currentFadeState = FadeState.Static;
                        CurrentAlpha = 0f;
                        OverlayColor = _originalColor.WithAlpha(CurrentAlpha);
                        FadeOutCompleted?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        CurrentAlpha = 1f - ((float)(_elapsed.TotalSeconds / _fadeDuration.TotalSeconds));
                        OverlayColor = _originalColor.WithAlpha(CurrentAlpha);
                    }

                    break;
            }
        }
    }
}
