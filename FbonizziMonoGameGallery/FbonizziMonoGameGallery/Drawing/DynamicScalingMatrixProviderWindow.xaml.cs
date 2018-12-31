using System;
using System.ComponentModel;
using System.Windows;

namespace FbonizziMonoGameGallery.Drawing
{
    public partial class DynamicScalingMatrixProviderWindow : Window
    {
        private readonly DynamicScalingMatrixProviderGame _game;

        public DynamicScalingMatrixProviderWindow()
        {
            InitializeComponent();

            _game = new DynamicScalingMatrixProviderGame(
                new WpfScreenSizeChangedNotifier(this),
                (float)Width,
                (float)Height);

            GameGrid.Children.Add(_game);
        }

        protected override void OnInitialized(EventArgs e)
        {
            
            base.OnInitialized(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _game.Dispose();
            base.OnClosing(e);
        }
    }
}
