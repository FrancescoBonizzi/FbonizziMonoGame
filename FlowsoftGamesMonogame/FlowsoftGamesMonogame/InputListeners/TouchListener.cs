using FbonizziMonogame.Drawing.ViewportAdapters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace FbonizziMonogame.InputListeners
{
    public class TouchListener : IInputListener
    {
        public TouchListener(ScalingViewportAdapter viewportAdapter)
        {
            ViewportAdapter = viewportAdapter;
        }

        public ScalingViewportAdapter ViewportAdapter { get; set; }

        public event EventHandler<TouchEventArgs> TouchStarted;
        public event EventHandler<TouchEventArgs> TouchEnded;
        public event EventHandler<TouchEventArgs> TouchMoved;
        public event EventHandler<TouchEventArgs> TouchCancelled;

        public void Update(GameTime gameTime)
        {
            var touchCollection = TouchPanel.GetState();

            foreach (var touchLocation in touchCollection)
            {
                switch (touchLocation.State)
                {
                    case TouchLocationState.Pressed:
                        TouchStarted?.Invoke(this, new TouchEventArgs(ViewportAdapter, gameTime.TotalGameTime, touchLocation));
                        break;
                    case TouchLocationState.Moved:
                        TouchMoved?.Invoke(this, new TouchEventArgs(ViewportAdapter, gameTime.TotalGameTime, touchLocation));
                        break;
                    case TouchLocationState.Released:
                        TouchEnded?.Invoke(this, new TouchEventArgs(ViewportAdapter, gameTime.TotalGameTime, touchLocation));
                        break;
                    case TouchLocationState.Invalid:
                        TouchCancelled?.Invoke(this, new TouchEventArgs(ViewportAdapter, gameTime.TotalGameTime, touchLocation));
                        break;
                }
            }
        }
    }
}
