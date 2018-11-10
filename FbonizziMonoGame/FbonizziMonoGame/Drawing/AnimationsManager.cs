using FbonizziMonogame.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FbonizziMonogame.Drawing
{
    public class AnimationsManager
    {
        private IDictionary<string, SpriteAnimation> _animations;
        public string CurrentAnimationName { get; private set; }
        public SpriteAnimation CurrentAnimation
            => _animations[CurrentAnimationName];

        public AnimationsManager()
        {
            _animations = new Dictionary<string, SpriteAnimation>();
        }

        public AnimationsManager AddAnimation(
           string animationKey,
           SpriteAnimation animation)
        {
            if (animation == null)
                throw new ArgumentNullException(nameof(animation));

            _animations.Add(animationKey, animation);
            return this;
        }

        public void PlayAnimation(string animationKey)
        {
            if (CurrentAnimationName == animationKey)
                return;

            CurrentAnimationName = animationKey;
            _animations[CurrentAnimationName].Play();
        }

        public SpriteAnimation GetAnimation(string animationName)
            => _animations[animationName];

        public void Update(TimeSpan elapsed)
            => _animations[CurrentAnimationName].Update(elapsed);

        public void Draw(
            SpriteBatch spriteBatch,
            DrawingInfos spatialInfos)
        {
            if (CurrentAnimationName == null)
                return;

            spriteBatch.Draw(
                _animations[CurrentAnimationName].CurrentFrame,
                spatialInfos);
        }

    }
}
