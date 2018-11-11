using FbonizziMonoGame.UI;

namespace FbonizziMonoGame.Extensions
{
    public static class StringsExtensions
    {
        public static string GetTranslation(this string stringKey)
            => MultilanguageStrings.Get(stringKey);
    }
}
