using Android.App;
using Android.Content;
using FbonizziMonoGame.PlatformAbstractions;
using System;

namespace FbonizziMonoGameAndroid
{
    /// <summary>
    /// Web page opener
    /// </summary>
    public class AndroidWebPageOpener : IWebPageOpener
    {
        private readonly Activity _activity;

        /// <summary>
        /// Web page opener
        /// </summary>
        /// <param name="activity"></param>
        public AndroidWebPageOpener(Activity activity)
        {
            _activity = activity ?? throw new ArgumentNullException(nameof(activity));
        }

        /// <summary>
        /// Opens a web page in an Android application
        /// </summary>
        /// <param name="uri"></param>
        public void OpenWebpage(Uri uri)
        {
            var uriToOpen = Android.Net.Uri.Parse(uri.ToString());
            var intent = new Intent(Intent.ActionView, uriToOpen);
            _activity.StartActivity(intent);
        }
    }
}
