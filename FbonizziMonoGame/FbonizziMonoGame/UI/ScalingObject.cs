using System;

namespace FbonizziMonogame.UI
{
    public class ScalingObject
    {
        private float _minScale;
        private float _maxScale;
        private float _scalingSpeed;

        private TimeSpan _totalElapsed;

        private readonly Func<float, float, float, float> _deltaScaleFunctionOverTime =
            MathHelperExtensions.GenerateDeltaOverTimeSin;

        public float Scale { get; private set; }

        public ScalingObject(
            float minScale = 0.8f,
            float maxScale = 1.2f,
            float scalingSpeed = 2.5f)
        {
            _minScale = minScale;
            _maxScale = maxScale;
            _scalingSpeed = scalingSpeed;
            _totalElapsed = TimeSpan.Zero;
        }

        public void Update(TimeSpan elapsed)
        {
            _totalElapsed += elapsed;
            Scale = _deltaScaleFunctionOverTime((float)(_totalElapsed.TotalSeconds * _scalingSpeed), _minScale, _maxScale);
        }
    }
}
