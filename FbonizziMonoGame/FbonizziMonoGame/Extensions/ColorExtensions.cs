using Microsoft.Xna.Framework;

namespace FbonizziMonogame.Extensions
{
    public static class ColorExtensions
    {
        public static Color WithAlpha(this Color color, float alpha)
            => color * alpha;
    }
}
