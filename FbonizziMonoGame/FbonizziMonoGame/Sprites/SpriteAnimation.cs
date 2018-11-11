using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FbonizziMonoGame
{
    /// <summary>
    /// Una lista di sprite in successione rappresenta un'animazione.
    /// </summary>
    public class SpriteAnimation
    {
        public Sprite[] Frames { get; private set; }

        private TimeSpan _currentFrameElapsed = TimeSpan.Zero;
        private int _currentFrameIndex = 0;
        private int _lastFrameIndex;
        private int _framesCount;

        /// <summary>
        /// Costruttore utilizzato per "clonare" un'animazione
        /// </summary>
        /// <param name="animation"></param>
        public SpriteAnimation(SpriteAnimation animation)
            : this(animation.Frames, animation.FrameDuration, animation.IsAnimationLooped)
        {

        }

        public SpriteAnimation(
            IEnumerable<Sprite> frames,
            TimeSpan frameDuration,
            bool isAnimationLooped = true)
        {
            if (frames == null)
                throw new ArgumentNullException(nameof(frames));

            Frames = frames.ToArray();
            IsAnimationLooped = isAnimationLooped;
            FrameDuration = frameDuration;
            _framesCount = Frames.Length;
            _lastFrameIndex = _framesCount - 1;
        }

        public bool IsAnimationLooped { get; set; }
        public bool HasFinishedPlaying { get; internal set; }
        
        public int CurrentFrameWidth 
            => Frames[_currentFrameIndex].Width;

        public int CurrentFrameHeight 
            => Frames[_currentFrameIndex].Height;
        
        public TimeSpan FrameDuration { get; set; }

        public Rectangle CurrentFrameRectangle 
            => Frames[_currentFrameIndex].SourceRectangle;

        public Sprite FirstFrameSprite 
            =>Frames[0];

        public void Play()
        {
            _currentFrameIndex = 0;
            HasFinishedPlaying = false;
        }

        public void Stop()
        {
            _currentFrameIndex = 0;
            HasFinishedPlaying = true;
        }
        
        public void Update(TimeSpan elapsed)
        {
            if (HasFinishedPlaying)
                return;

            _currentFrameElapsed += elapsed;

            if (_currentFrameElapsed >= FrameDuration)
            {
                ++_currentFrameIndex;

                if (_currentFrameIndex >= _framesCount)
                {
                    if (IsAnimationLooped)
                    {
                        _currentFrameIndex = 0;
                    }
                    else
                    {
                        _currentFrameIndex = _lastFrameIndex;
                        HasFinishedPlaying = true;
                    }
                }

                _currentFrameElapsed = TimeSpan.Zero;
            }
        }

        public Sprite CurrentFrame
            => Frames[_currentFrameIndex];
    }
}
