using FbonizziMonoGame.Extensions;
using FbonizziMonoGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FbonizziMonoGame.Particles
{
    /// <summary>
    /// Particle generator
    /// </summary>
    public abstract class ParticleGenerator
    {
        private readonly Sprite _sprite;
        private readonly Color _particleOverlayColor = Color.White;
        private Vector2 _origin;

        private Particle[] _activeParticles;
        private Queue<Particle> _freeParticles;
        
        /// <summary>
        /// Density of particles each generation
        /// </summary>
        protected abstract int Density { get; }

        /// <summary>
        /// Minimum number of particles
        /// </summary>
        protected abstract int MinNumParticles { get; }

        /// <summary>
        /// Maximum number of particles
        /// </summary>
        protected abstract int MaxNumParticles { get; }

        /// <summary>
        /// Minimum initial speed of the particles
        /// </summary>
        protected abstract float MinInitialSpeed { get; }

        /// <summary>
        /// Maximum initial speed of the particles
        /// </summary>
        protected abstract float MaxInitialSpeed { get; }

        /// <summary>
        /// Minimum acceleration of the particles
        /// </summary>
        protected abstract float MinAcceleration { get; }

        /// <summary>
        /// Maximum acceleration of the particles
        /// </summary>
        protected abstract float MaxAcceleration { get; }

        /// <summary>
        /// Minimum rotation speed of the particles
        /// </summary>
        protected abstract float MinRotationSpeed { get; }

        /// <summary>
        /// Maximum rotation speed of the particles
        /// </summary>
        protected abstract float MaxRotationSpeed { get; }

        /// <summary>
        /// Minimum lifetime of the particles
        /// </summary>
        protected abstract TimeSpan MinLifetime { get; }

        /// <summary>
        /// Maximum lifetime of the particles
        /// </summary>
        protected abstract TimeSpan MaxLifetime { get; }

        /// <summary>
        /// Minimum particles scale
        /// </summary>
        protected abstract float MinScale { get; }

        /// <summary>
        /// Maximum particles scale
        /// </summary>
        protected abstract float MaxScale { get; }

        /// <summary>
        /// Minimum particles spawn angle
        /// </summary>
        protected abstract float MinSpawnAngle { get; }

        /// <summary>
        /// Maximum particles spawn angle
        /// </summary>
        protected abstract float MaxSpawnAngle { get; }

        /// <summary>
        /// A particle generator with a single sprite as particle template
        /// </summary>
        /// <param name="particleSprite"></param>
        public ParticleGenerator(Sprite particleSprite)
        {
            _sprite = particleSprite ?? throw new ArgumentNullException(nameof(particleSprite));
            
            _origin = new Vector2(
                particleSprite.Width / 2,
                particleSprite.Height / 2);

            Initialize();
        }

        /// <summary>
        /// Initializes the particle generator, creating all particles and putting them in the dead particles queue
        /// </summary>
        private void Initialize()
        {
            _activeParticles = new Particle[Density * MaxNumParticles];
            _freeParticles = new Queue<Particle>(Density * MaxNumParticles);
            for (int i = 0; i < _activeParticles.Length; ++i)
            {
                _activeParticles[i] = new Particle();
                _freeParticles.Enqueue(_activeParticles[i]);
            }
        }

        /// <summary>
        /// Adds new paticles taking them from the dead particles queue
        /// </summary>
        /// <param name="where"></param>
        public void AddParticles(Vector2 where)
        {
            int numParticles = Numbers.RandomBetween(
                MinNumParticles,
                MaxNumParticles);

            for (int i = 0; i < numParticles && _freeParticles.Count > 0; ++i)
            {
                Particle p = _freeParticles.Dequeue();
                InitializeParticle(p, where);
            }
        }

        /// <summary>
        /// True if there are active particles
        /// </summary>
        public bool HasActiveParticles
            => _freeParticles.Count != _activeParticles.Count();

        /// <summary>
        /// Initializes a single particle with random values in the range of the properties defined in this class
        /// </summary>
        /// <param name="p"></param>
        /// <param name="where"></param>
        private void InitializeParticle(
            Particle p,
            Vector2 where)
        {
            Vector2 direction = PickRandomDirection();

            // Valori casuali per inizializzare la particella
            float velocity = Numbers.RandomBetween(MinInitialSpeed, MaxInitialSpeed);
            float acceleration = Numbers.RandomBetween(MinAcceleration, MaxAcceleration);
            TimeSpan lifetime = Numbers.RandomBetween(MinLifetime, MaxLifetime);
            float scale = Numbers.RandomBetween(MinScale, MaxScale);
            float rotationSpeed = Numbers.RandomBetween(MinRotationSpeed, MaxRotationSpeed);
            float initialRotation = Numbers.RandomBetween(0, MathHelper.TwoPi);

            p.Initialize(
                where,
                velocity * direction,
                acceleration * direction,
                initialRotation,
                rotationSpeed,
                _particleOverlayColor,
                scale,
                lifetime);
        }

        /// <summary>
        /// It returns a random direction in the interval [MinSpawnAngle; MaxSpawnAngle]
        /// </summary>
        /// <returns></returns>
        private Vector2 PickRandomDirection()
        {
            float radians = Numbers.RandomBetween(
                MathHelper.ToRadians(MinSpawnAngle),
                MathHelper.ToRadians(MaxSpawnAngle));

            Vector2 direction = new Vector2(
                (float)Math.Cos(radians),
                -(float)Math.Sin(radians));

            return direction;
        }

        /// <summary>
        /// Manages the particle generator logic
        /// </summary>
        /// <param name="elapsed"></param>
        public virtual void Update(TimeSpan elapsed)
        {
            for (int i = 0; i < _activeParticles.Length; ++i)
            {
                Particle p = _activeParticles[i];

                if (p.IsActive)
                {
                    p.Update(elapsed);

                    if (!p.IsActive)
                        _freeParticles.Enqueue(p);
                }
            }
        }

        /// <summary>
        /// Draws the effect
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _activeParticles.Length; ++i)
            {
                Particle p = _activeParticles[i];

                if (!p.IsActive)
                    continue;

                // Normalized lifetime is a [0; 1] value that means:
                //  0: just born
                // .5: at half of its life
                //  1: his life is ended
                // I use this value to calculate opacity and scale
                double normalizedLifetime = p.TimeSinceStart.TotalSeconds / p.LifeTime.TotalSeconds;

                // Particles must do fadein/fadeout in relation to its lifetime
                // Here I ensure that:
                // - When its dead, it's opacity is 0
                // - Its max opacity (1) it at half of its life
                double alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
                p.OverlayColor = Color.White.WithAlpha((float)alpha);

                // A particle changes their scale in relation to its lifetime:
                // It begin with 75% of their dimension and it arrives to 100% when dead
                double scale = p.InitalScale * (0.75 + 0.25 * normalizedLifetime);
                p.Scale = (float)scale;

                spriteBatch.Draw(_sprite, p);
            }
        }

    }
}
