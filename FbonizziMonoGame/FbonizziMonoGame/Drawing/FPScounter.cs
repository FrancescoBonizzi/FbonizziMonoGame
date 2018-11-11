using FbonizziMonoGame.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Text;

namespace FbonizziMonoGame.Drawing
{
    public class FPScounter : DrawingInfos
    {
        private static readonly TimeSpan _oneSecondTimeSpan = new TimeSpan(0, 0, 1);
        private int _framesCounter;
        private TimeSpan _timer = _oneSecondTimeSpan;
        private StringBuilder _fpsCache = new StringBuilder();

        public int FramesPerSecond { get; private set; }

        public void Update(GameTime gameTime)
        {
            _timer += gameTime.ElapsedGameTime;
            if (_timer <= _oneSecondTimeSpan)
                return;

            FramesPerSecond = _framesCounter;
            _framesCounter = 0;
            _timer -= _oneSecondTimeSpan;
            _fpsCache.Clear();
            _fpsCache.Append("FPS: ");
            _fpsCache.AppendNumber(_framesCounter);
        }

        public string CurrentFPS
            => _fpsCache.ToString();

        public void Draw()
        {
            ++_framesCounter;
        }
    }
}
