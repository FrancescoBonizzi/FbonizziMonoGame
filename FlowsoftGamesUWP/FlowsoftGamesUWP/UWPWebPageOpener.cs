using FlowsoftGamesMonogame.UI;
using System;

namespace FlowsoftGamesUWP
{
    public class UWPWebPageOpener : IWebPageOpener
    {
        public void OpenWebpage(Uri uri)
            => Windows.System.Launcher.LaunchUriAsync(uri).GetAwaiter().GetResult();
    }
}
