using FbonizziMonogame.UI;
using System;

namespace FbonizziMonoGameUWP
{
    public class UWPWebPageOpener : IWebPageOpener
    {
        public void OpenWebpage(Uri uri)
            => Windows.System.Launcher.LaunchUriAsync(uri).GetAwaiter().GetResult();
    }
}
