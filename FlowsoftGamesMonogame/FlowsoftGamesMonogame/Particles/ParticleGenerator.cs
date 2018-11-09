using FbonizziGames.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FbonizziGames.Particles
{
    /// <summary>
    /// Motore particellare
    /// </summary>
    public abstract class ParticleGenerator
    {
        private Sprite _sprite;
        private Color _particleOverlayColor = Color.White;
        private Vector2 _origin;

        private Particle[] _activeParticles;
        private Queue<Particle> _freeParticles;
        
        protected abstract int Density { get; }

        protected abstract int MinNumParticles { get; }
        protected abstract int MaxNumParticles { get; }

        protected abstract float MinInitialSpeed { get; }
        protected abstract float MaxInitialSpeed { get; }

        protected abstract float MinAcceleration { get; }
        protected abstract float MaxAcceleration { get; }

        protected abstract float MinRotationSpeed { get; }
        protected abstract float MaxRotationSpeed { get; }

        protected abstract TimeSpan MinLifetime { get; }
        protected abstract TimeSpan MaxLifetime { get; }

        protected abstract float MinScale { get; }
        protected abstract float MaxScale { get; }

        protected abstract float MinSpawnAngle { get; }
        protected abstract float MaxSpawnAngle { get; }

        public ParticleGenerator(Sprite particleSprite)
        {
            _sprite = particleSprite ?? throw new ArgumentNullException(nameof(particleSprite));
            
            _origin = new Vector2(
                particleSprite.Width / 2,
                particleSprite.Height / 2);

            Initialize();
        }

        /// <summary>
        /// Inizializza il generatore, creando le particelle e la coda di particelle morte
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
        /// Prende, se ci sono numParticles morte e le reinizializza
        /// </summary>
        /// <param name="where"></param>
        public void AddParticles(Vector2 where)
        {
            int numParticles = MathHelperExtensions.RandomBetween(
                MinNumParticles,
                MaxNumParticles);

            for (int i = 0; i < numParticles && _freeParticles.Count > 0; ++i)
            {
                Particle p = _freeParticles.Dequeue();
                InitializeParticle(p, where);
            }
        }

        public bool HasActiveParticles
            => _freeParticles.Count != _activeParticles.Count();

        /// <summary>
        /// Inizializza una singola particella con valori casuali entro i range stabiliti nell'override delle varie proprietà
        /// </summary>
        /// <param name="p"></param>
        /// <param name="where"></param>
        private void InitializeParticle(
            Particle p,
            Vector2 where)
        {
            Vector2 direction = PickRandomDirection();

            // Valori casuali per inizializzare la particella
            float velocity = MathHelperExtensions.RandomBetween(MinInitialSpeed, MaxInitialSpeed);
            float acceleration = MathHelperExtensions.RandomBetween(MinAcceleration, MaxAcceleration);
            TimeSpan lifetime = MathHelperExtensions.RandomBetween(MinLifetime, MaxLifetime);
            float scale = MathHelperExtensions.RandomBetween(MinScale, MaxScale);
            float rotationSpeed = MathHelperExtensions.RandomBetween(MinRotationSpeed, MaxRotationSpeed);
            float initialRotation = MathHelperExtensions.RandomBetween(0, MathHelper.TwoPi);

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
        /// Ritorna una direzione casuale entro il range [_minSpawnAngle; _maxSpawnAngle]
        /// </summary>
        /// <returns></returns>
        private Vector2 PickRandomDirection()
        {
            float radians = MathHelperExtensions.RandomBetween(
                MathHelper.ToRadians(MinSpawnAngle),
                MathHelper.ToRadians(MaxSpawnAngle));

            Vector2 direction = new Vector2(
                (float)Math.Cos(radians),
                -(float)Math.Sin(radians));

            return direction;
        }

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

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _activeParticles.Length; ++i)
            {
                Particle p = _activeParticles[i];

                if (!p.IsActive)
                    continue;

                // Il valore di vita normalizzato è un valore tra 0 e 1 che quantifica
                // la vita di una particella.
                //  0: appena nata
                // .5: a metà della sua vita
                //  1: la sua vita è terminata
                // Questo valore viene usato per calcolarne l'opacità e la scala
                double normalizedLifetime = p.TimeSinceStart.TotalSeconds / p.LifeTime.TotalSeconds;

                // Le particelle devono fare fadein e fadeout, questa è la funzione che ne calcola l'opacità
                // in relazione alla quantificazione della sua vita normalizzata.
                // Con questa formula mi assicuro che:
                // - quando la sua vita è 0 o 1, alpha sia zero.
                // - la massima opacità sia raggiunta quando è a metà della sua vita, come una stella
                // - dato che in questo modo, la massima opacità verrebbe .25, considerando che la vita è a metà a 0.5 (0.5 * (1 - 0.5)) = 0.25,
                //   scalo l'equazione di 4, così che la massima opacità diventi 1.0 
                double alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
                p.OverlayColor = Color.White.WithAlpha((float)alpha);

                // Le particelle devono anche aumentare e diminuire in scala:
                // questa è la funzione che la determina.
                // Iniziano al 75% della loro dimensione e arrivano al 100% quando muoiono
                double scale = p.InitalScale * (0.75 + 0.25 * normalizedLifetime);
                p.Scale = (float)scale;

                spriteBatch.Draw(_sprite, p);
            }
        }

    }
}
