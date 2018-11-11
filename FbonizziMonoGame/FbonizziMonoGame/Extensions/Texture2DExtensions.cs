using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FbonizziMonoGame.Extensions
{
    public static class Texture2DExtensions
    {
        public static Texture2D CreateTexture(
            this Texture2D texture, 
            GraphicsDevice graphics, 
            Rectangle cropRectangle)
        {
            Texture2D tex = new Texture2D(graphics, cropRectangle.Width, cropRectangle.Height);
            int count = cropRectangle.Width * cropRectangle.Height;
            Color[] data = new Color[count];
            texture.GetData(0, cropRectangle, data, 0, count);
            tex.SetData(data);
            return tex;
        }
    }
}
