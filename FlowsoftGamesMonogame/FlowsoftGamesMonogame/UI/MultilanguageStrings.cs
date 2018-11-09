using System.Collections.Generic;
using System.Globalization;

namespace FbonizziGames.UI
{
    public static class MultilanguageStrings
    {
        private static IDictionary<string, string> _stringsRepository = new Dictionary<string, string>();

        public const string ItalianLocaleName = "it-IT";

        public static bool IsItalian(string localeName)
            => localeName == ItalianLocaleName;

        public static bool IsItalian(CultureInfo culture)
            => IsItalian(culture.Name);

        // Non voglio fare rompere se cerco di aggiungere chiavi uguali
        // perché col lifecycle di Android può ripassare di qua quando 
        // ancora il processo non unloadato
        public static void Add(string key, string value)
            => _stringsRepository[key] = value;

        public static string Get(string key)
            => _stringsRepository[key];
    }
}
