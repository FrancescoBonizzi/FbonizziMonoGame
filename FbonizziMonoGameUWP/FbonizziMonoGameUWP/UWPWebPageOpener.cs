using FbonizziMonoGame.PlatformAbstractions;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace FbonizziMonoGameUWP
{
    /// <summary>
    /// UWP implementation of <see cref="IWebPageOpener"/>
    /// </summary>
    public class UWPWebPageOpener : IWebPageOpener
    {
        private readonly Window _uiWindow;

        /// <summary>
        /// Construct the UWPWebPageOpener given a reference to the UI thread Window
        /// </summary>
        /// <param name="uiThreadWindow"></param>
        public UWPWebPageOpener(Window uiThreadWindow)
        {
            _uiWindow = uiThreadWindow;
        }

        /// <summary>
        /// Opens an URI in browser
        /// </summary>
        /// <param name="uri"></param>
        public void OpenWebpage(Uri uri)
        {
            var currentDispatcher = _uiWindow.Dispatcher;
            var task = currentDispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                async () => await Windows.System.Launcher.LaunchUriAsync(uri));
            Task.WaitAll(task.AsTask());
        }
    }
}
