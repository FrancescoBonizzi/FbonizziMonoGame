using Microsoft.Xna.Framework;
using System;

namespace FbonizziMonogame.UI
{
    public class ColorChangingObject
    {
        private Color _firstColor;
        private Color _secondColor;
        public double ChangingSpeed { get; set; }

        private TimeSpan _totalElapsed;

        public Color Color { get; private set; }

        public ColorChangingObject(
            Color firstColor,
            Color secondColor,
            double changingSpeed = 2.5)
        {
            _firstColor = firstColor;
            _secondColor = secondColor;
            ChangingSpeed = changingSpeed;
            _totalElapsed = TimeSpan.Zero;
        }

        public void Update(TimeSpan elapsed)
        {
            _totalElapsed += elapsed;
            Color = Color.Lerp(_firstColor, _secondColor, (float)Math.Sin(_totalElapsed.TotalSeconds * ChangingSpeed));
        }
    }
}
