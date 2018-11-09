using System.Globalization;

namespace FlowsoftGamesAndroidToolkit
{
    public class GameCultureProvider
    {
        public static CultureInfo GetCurrentCulture()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLocale = androidLocale.ToString().Replace("_", "-");
            var currentCulture = CultureInfo.CreateSpecificCulture(netLocale);
            return currentCulture;
        }
    }
}
