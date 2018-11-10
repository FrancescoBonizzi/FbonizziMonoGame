using FbonizziMonogame.UI;
using System;

namespace FbonizziMonoGameWindowsDesktop
{
    public class WindowsWebSiteOpener : IWebPageOpener
    {
        public void OpenWebpage(Uri uri)
            => System.Diagnostics.Process.Start(uri.ToString());
    }
}
