using System.Windows;

namespace FbonizziMonoGameGallery
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ParticleGeneratorTimeEffectButton_Click(object sender, RoutedEventArgs e)
        {
            var gallery = new ParticleGeneratorTimedEffect.ParticleGeneratorTimedEffectWindow();
            gallery.ShowDialog();
        }

        private void DynamicScalingMatrixProviderButton_Click(object sender, RoutedEventArgs e)
        {
            var gallery = new Drawing.DynamicScalingMatrixProviderWindow();
            gallery.ShowDialog();
        }
    }
}
