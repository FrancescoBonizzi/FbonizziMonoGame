using FbonizziMonoGame.PlatformAbstractions;
using System;

namespace FbonizziMonoGameWindowsDesktop
{
    /// <summary>
    /// Opens an URI with System.Diagnostics.Process.Start
    /// </summary>
    public class WindowsWebSiteOpener : IWebPageOpener
    {
        /// <summary>
        /// Opens an URI with System.Diagnostics.Process.Start
        /// </summary>
        /// <param name="uri"></param>
        public void OpenWebpage(Uri uri)
            => System.Diagnostics.Process.Start(uri.ToString());
    }
}
