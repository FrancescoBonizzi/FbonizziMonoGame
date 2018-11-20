using System.Globalization;

namespace FbonizziMonoGameAndroid
{
    /// <summary>
    /// It returns the current app culture
    /// </summary>
    public class GameCultureProvider
    {
        /// <summary>
        /// It returns the current app culture
        /// </summary>
        /// <returns></returns>
        public static CultureInfo GetCurrentCulture()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLocale = androidLocale.ToString().Replace("_", "-");
            var currentCulture = CultureInfo.CreateSpecificCulture(netLocale);
            return currentCulture;
        }
    }
}
