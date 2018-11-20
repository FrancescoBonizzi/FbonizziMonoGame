using Android.App;
using Android.Views;

namespace FbonizziMonoGameAndroid
{
    /// <summary>
    /// Sets UI options for a game in an Android Activity
    /// </summary>
    public static class GameWindowOptions
    {
        /// <summary>
        /// Sets UI options for a game in an Android Activity
        /// </summary>
        /// <param name="activity"></param>
        public static void SetGameOptions(this Activity activity)
        {
            var decorView = activity.Window.DecorView;
            var uiOptions = (int)decorView.SystemUiVisibility;
            var newUiOptions = uiOptions;

            newUiOptions |= (int)SystemUiFlags.LowProfile;
            newUiOptions |= (int)SystemUiFlags.Fullscreen;
            newUiOptions |= (int)SystemUiFlags.HideNavigation;
            newUiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
            activity.Window.AddFlags(WindowManagerFlags.Fullscreen);
        }
    }
}
