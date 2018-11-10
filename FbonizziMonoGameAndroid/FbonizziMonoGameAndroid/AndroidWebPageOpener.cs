using Android.App;
using Android.Content;
using FbonizziMonogame.UI;
using System;

namespace FlowsoftGamesAndroid
{
    public class AndroidWebPageOpener : IWebPageOpener
    {
        private readonly Activity _activity;

        public AndroidWebPageOpener(Activity activity)
        {
            _activity = activity ?? throw new ArgumentNullException(nameof(activity));
        }

        public void OpenWebpage(Uri uri)
        {
            var uriToOpen = Android.Net.Uri.Parse(uri.ToString());
            var intent = new Intent(Intent.ActionView, uriToOpen);
            _activity.StartActivity(intent);
        }
    }
}
