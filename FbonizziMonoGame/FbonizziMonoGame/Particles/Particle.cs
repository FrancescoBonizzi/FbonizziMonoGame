using FbonizziMonoGame.Drawing;
using Microsoft.Xna.Framework;
using System;

namespace FbonizziMonoGame.Particles
{
    /// <summary>
    /// Una particella da utilizzare in un motore particellare,
    /// caratterizzata da proprietà spaziali e ciclo di vita.
    /// </summary>
    public class Particle : DrawingInfos
    {
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public TimeSpan TimeSinceStart { get; set; }
        public float InitalScale { get; set; }

        public float RotationSpeed { get; set; }
        public TimeSpan LifeTime { get; set; }

        public void Initialize(
            Vector2 position,
            Vector2 velocity,
            Vector2 acceleration,
            float initialRotation,
            float rotationSpeed,
            Color color,
            float scale,
            TimeSpan lifetime)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
            Rotation = initialRotation;
            RotationSpeed = rotationSpeed;
            OverlayColor = color;
            InitalScale = scale;
            Scale = InitalScale;
            LifeTime = lifetime;

            TimeSinceStart = TimeSpan.Zero;
        }

        public bool IsActive 
            => TimeSinceStart < LifeTime || OverlayColor.A < 0;

        public void Update(TimeSpan elapsed)
        {
            float elapsedSeconds = (float)elapsed.TotalSeconds;
            Velocity += Acceleration * elapsedSeconds;
            Position += Velocity * elapsedSeconds;
            Rotation += RotationSpeed * elapsedSeconds;

            TimeSinceStart += elapsed;
        }
    }
}
