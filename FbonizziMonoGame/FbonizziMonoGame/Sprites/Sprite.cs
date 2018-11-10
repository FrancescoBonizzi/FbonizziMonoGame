using FbonizziMonogame.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FbonizziMonogame
{
    /// <summary>
    /// La sprite è un oggetto che contiene le informazioni per identificare un'immagine
    /// all'interno di un'immagine più grande che contiene n sprite.
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// Una reference all'immagine in cui la sprite è contenuta.
        /// </summary>
        public Texture2D Sheet { get; }

        /// <summary>
        /// Il rettangolo che identifica la sprite nello spritesheet
        /// </summary>
        public Rectangle SourceRectangle { get; }

        public Vector2 SpriteCenter { get; }

        public int Width { get; }
        public int Height { get; }

        public Sprite(
            SpriteDescription spriteDescription,
            Texture2D sheet)
        {
            Sheet = sheet ?? throw new ArgumentNullException(nameof(sheet));
            SourceRectangle = new Rectangle(
                spriteDescription.X, spriteDescription.Y,
                spriteDescription.Width, spriteDescription.Height);
            Width = spriteDescription.Width;
            Height = spriteDescription.Height;
            SpriteCenter = new Vector2(
                SourceRectangle.Width / 2,
                SourceRectangle.Height / 2);
        }
        
    }
}
