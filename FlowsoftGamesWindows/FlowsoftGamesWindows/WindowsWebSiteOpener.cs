using FbonizziGames.UI;
using System;

namespace FlowsoftGamesWindows
{
    public class WindowsWebSiteOpener : IWebPageOpener
    {
        public void OpenWebpage(Uri uri)
            => System.Diagnostics.Process.Start(uri.ToString());
    }
}
