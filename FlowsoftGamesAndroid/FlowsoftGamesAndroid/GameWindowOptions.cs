using Android.App;
using Android.Views;

namespace FlowsoftGamesAndroidToolkit
{
    public static class GameWindowOptions
    {
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
