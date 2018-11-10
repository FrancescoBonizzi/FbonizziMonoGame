using FbonizziMonogame.UI;

namespace FbonizziMonogame.Extensions
{
    public static class StringsExtensions
    {
        public static string GetTranslation(this string stringKey)
            => MultilanguageStrings.Get(stringKey);
    }
}
