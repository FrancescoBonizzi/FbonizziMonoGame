using FbonizziMonoGame.Drawing.Abstractions;
using System;
using System.Windows;

namespace FbonizziMonoGameGallery.Drawing
{
    public class WpfScreenSizeChangedNotifier : IScreenSizeChangedNotifier
    {
        public event EventHandler OnScreenSizeChanged;

        public WpfScreenSizeChangedNotifier(Window gameWindow)
        {
            if (gameWindow == null)
                throw new ArgumentNullException(nameof(gameWindow));

            gameWindow.SizeChanged += gameWindow_SizeChanged;
        }

        private void gameWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            OnScreenSizeChanged?.Invoke(sender, e);
        }
    }
}
