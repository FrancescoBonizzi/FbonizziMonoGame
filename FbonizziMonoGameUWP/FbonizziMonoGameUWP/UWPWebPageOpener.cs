using FbonizziMonoGame.PlatformAbstractions;
using System;

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
            => Windows.System.Launcher.LaunchUriAsync(uri).GetAwaiter().GetResult();
    }
}
