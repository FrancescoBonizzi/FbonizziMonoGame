using FbonizziMonogame.Drawing;
using FbonizziMonogame.Drawing.ViewportAdapters;
using FbonizziMonogame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Playground
{
    public class DialogButtonsTest : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private Dialog _dialog;
        private string _whatHasBeenClicked = "No clicks yet";
        private BoxingViewportAdapter _viewportAdapter;
        private const int _windowWidth = 500;
        private const int _windowHeight = 500;

        public DialogButtonsTest()
        {
            Window.AllowUserResizing = true;

            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = _windowWidth,
                PreferredBackBufferHeight = _windowHeight
            };

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            _viewportAdapter = new BoxingViewportAdapter(Window, _graphics.GraphicsDevice, 500, 500);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("TestFont");

            var buttonA = new ButtonWithText(
                _font,
                "Yes!",
                new Rectangle(150, 280, 80, 50),
                Color.Gray,
                Color.White,
                Color.DarkGray,
                () => _whatHasBeenClicked = "You clicked yes :-)",
                10f);

            var buttonB = new ButtonWithText(
                _font,
                "No.",
                new Rectangle(270, 280, 80, 50),
                Color.Gray,
                Color.White,
                Color.DarkGray,
                () => _whatHasBeenClicked = "You clicked no :-(",
                10f);
            buttonB.TextScale = buttonA.TextScale;

            _dialog = new Dialog(
                title: "Would you like to test me?",
                font: _font,
                dialogWindowDefinition: new Rectangle(100, 100, 300, 300),
                titlePositionOffset: new Vector2(150, 50),
                backgroundColor: Color.White,
                backgroundShadowColor: Color.DarkGray,
                titleColor: Color.Black,
                titlePadding: 40f,
                buttons: new ButtonWithText[] { buttonA, buttonB });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                _dialog.HandleInput(new Vector2(
                    mouseState.Position.X,
                    mouseState.Position.Y));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _viewportAdapter.ScaleMatrix);
            _dialog.Draw(_spriteBatch);
            _spriteBatch.DrawString(_font, _whatHasBeenClicked, new Vector2(100, 30), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
