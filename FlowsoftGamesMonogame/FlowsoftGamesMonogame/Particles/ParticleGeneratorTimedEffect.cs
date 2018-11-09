using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FbonizziGames.Particles
{
    public class ParticleGeneratorTimedEffect
    {
        private ParticleGenerator _particleGenerator;
        private Vector2 _generatorPosition;

        private TimeSpan _generationInterval;
        private TimeSpan _effectAliveTime;
        private readonly TimeSpan _originalGenerationInterval;
        
        public ParticleGeneratorTimedEffect(
            ParticleGenerator particleGenerator,
            Vector2 startingPosition,
            TimeSpan generationInterval,
            TimeSpan effectAliveTime)
        {
            _generatorPosition = startingPosition;
            _particleGenerator = particleGenerator;
            _generationInterval = generationInterval;
            _originalGenerationInterval = generationInterval;
            _effectAliveTime = effectAliveTime;
            _particleGenerator.AddParticles(_generatorPosition);
        }

        public Vector2 SetGeneratorPosition(Vector2 newPosition)
            => _generatorPosition = newPosition;

        public bool Finished
            => !_particleGenerator.HasActiveParticles && _effectAliveTime <= TimeSpan.Zero;

        public void Update(TimeSpan elapsed)
        {
            _generationInterval -= elapsed;
            _effectAliveTime -= elapsed;
            _particleGenerator.Update(elapsed);

            if (_generationInterval <= TimeSpan.Zero 
                && _effectAliveTime > TimeSpan.Zero)
            {
                _generationInterval = _originalGenerationInterval;
                _particleGenerator.AddParticles(_generatorPosition);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
            => _particleGenerator.Draw(spriteBatch);
    }
}
