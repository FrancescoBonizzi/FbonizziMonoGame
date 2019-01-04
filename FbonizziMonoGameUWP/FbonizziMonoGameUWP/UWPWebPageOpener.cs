using FbonizziMonoGame.PlatformAbstractions;
using System;
using Windows.UI.Xaml;

namespace FbonizziMonoGameUWP
{
    /// <summary>
    /// UWP implementation of <see cref="IWebPageOpener"/>
    /// </summary>
    public class UWPWebPageOpener : IWebPageOpener
    {
        /// <summary>
        /// Opens an URI in browser
        /// </summary>
        /// <param name="uri"></param>
        public void OpenWebpage(Uri uri)
            => Window.Current.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                async () => await Windows.System.Launcher.LaunchUriAsync(uri))
            .GetAwaiter().GetResult();
    }
}
