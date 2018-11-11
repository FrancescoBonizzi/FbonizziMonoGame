using FbonizziMonoGame.Drawing.ViewportAdapters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace FbonizziMonoGame.InputListeners
{
    public class TouchEventArgs : EventArgs
    {
        public TouchEventArgs(
            ScalingViewportAdapter viewportAdapter, 
            TimeSpan time, 
            TouchLocation location)
        {
            ViewportAdapter = viewportAdapter;
            RawTouchLocation = location;
            Time = time;
            var pointPosition = viewportAdapter?.PointToScreen((int)location.Position.X, (int)location.Position.Y)
                ?? location.Position.ToPoint();
            Position = new Vector2(pointPosition.X, pointPosition.Y);
        }

        public ScalingViewportAdapter ViewportAdapter { get; }
        public TouchLocation RawTouchLocation { get; }
        public TimeSpan Time { get; }
        public Vector2 Position { get; }

        public override bool Equals(object other)
        {
            var args = other as TouchEventArgs;

            if (args == null)
                return false;

            return ReferenceEquals(this, args) || RawTouchLocation.Id.Equals(args.RawTouchLocation.Id);
        }

        public override int GetHashCode()
        {
            return RawTouchLocation.Id.GetHashCode();
        }
    }
}
