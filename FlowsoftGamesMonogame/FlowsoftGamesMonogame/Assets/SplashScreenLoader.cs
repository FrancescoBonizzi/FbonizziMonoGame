using FbonizziGames.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FbonizziGames.Assets
{
    public class SplashScreenLoader
    {
        private readonly Action _loadFunction;
        private readonly ContentManager _contentManager;
        private readonly string _splashScreenPath;
        private Sprite _splashScreenSprite;
        private FadeObject _splashScreenFadingObject;

        public TimeSpan MininumSplashScreenDuration { get; set; } = TimeSpan.FromSeconds(2);
        public event EventHandler Completed;

        public SplashScreenLoader(
            Action loadFunction,
            ContentManager contentManager,
            string splashScreenPath)
        {
            _loadFunction = loadFunction ?? throw new ArgumentNullException(nameof(loadFunction));
            _contentManager = contentManager ?? throw new ArgumentNullException(nameof(contentManager));
            _splashScreenPath = splashScreenPath ?? throw new ArgumentNullException(nameof(splashScreenPath));
        }

        public void Load()
        {
            var splashScreenTexture = _contentManager.Load<Texture2D>(_splashScreenPath);

            _splashScreenSprite = new Sprite(
                new SpriteDescription()
                {
                    X = 0, Y = 0,
                    Width = splashScreenTexture.Width,
                    Height = splashScreenTexture.Height,
                    Name = splashScreenTexture.Name
                },
                splashScreenTexture);
            _splashScreenFadingObject = new FadeObject(TimeSpan.FromSeconds(1), Color.White);
            _splashScreenFadingObject.FadeIn();
            _splashScreenFadingObject.FadeOutCompleted += _splashScreen_FadeOutCompleted;

            // Ritorno subito in modo da continuare in background l'esecuzione della funzione
            Task.Run(() => LoadAndManageSplashScreen());
        }

        private void _splashScreen_FadeOutCompleted(object sender, EventArgs e)
            => Completed?.Invoke(null, EventArgs.Empty);

        private void LoadAndManageSplashScreen()
        {
            var loadTimer = new Stopwatch();
            loadTimer.Start();
            _loadFunction();
            loadTimer.Stop();

            var lastingSplashScreenDuration = MininumSplashScreenDuration - loadTimer.Elapsed;
            if (lastingSplashScreenDuration > TimeSpan.Zero)
                Task.Delay(lastingSplashScreenDuration).GetAwaiter().GetResult();

            _splashScreenFadingObject.FadeOut();
        }

        public void Update(TimeSpan elapsed)
            => _splashScreenFadingObject.Update(elapsed);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _splashScreenSprite.Sheet,
                Vector2.Zero,
                _splashScreenFadingObject.OverlayColor);
        }
    }
}
