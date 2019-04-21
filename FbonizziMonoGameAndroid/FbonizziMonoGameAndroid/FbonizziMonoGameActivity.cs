using Android.OS;
using FbonizziMonoGame.PlatformAbstractions;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace FbonizziMonoGameAndroid
{
    /// <summary>
    /// A base class to hide some Android Activity boilerplate when defining a MonoGame game activity
    /// </summary>
    public abstract class FbonizziMonoGameActivity : AndroidGameActivity
    {
        private IFbonizziGame _game;

        /// <summary>
        /// Called when the activity is created
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            this.SetGameOptions();
            _game = StartGame(GameCultureProvider.GetCurrentCulture());
            _game.ExitGameRequested += Game_ExitGameRequested;
        }

        private void Game_ExitGameRequested(object sender, System.EventArgs e)
            => Game.Activity.MoveTaskToBack(true);

        /// <summary>
        /// Starts the game in this activity and returns the game instance.
        /// Called on the <see cref="OnCreate(Bundle)"/> method
        /// </summary>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        protected abstract IFbonizziGame StartGame(CultureInfo cultureInfo);

        /// <summary>
        /// Called when the current Android.Views.Window of the activity gains or loses focus.
        /// </summary>
        /// <param name="hasFocus"></param>
        public override void OnWindowFocusChanged(bool hasFocus)
        {
            if (hasFocus)
            {
                // To fix that with Android OREO 
                // the power button long press changes the client size area bounds
                // without considering the previous game options
                this.SetGameOptions();
            }

            base.OnWindowFocusChanged(hasFocus);
        }

        /// <summary>
        /// Called when the activity must the totally release its resources
        /// </summary>
        protected override void OnDestroy()
        {
            DisposeGame();
            _game = null;
            base.OnDestroy();
        }

        /// <summary>
        /// Releases the game unmanaged resources
        /// </summary>
        protected abstract void DisposeGame();

        /// <summary>
        /// Called when the activity is paused
        /// </summary>
        protected override void OnPause()
        {
            _game?.Pause();
            base.OnPause();
        }

        /// <summary>
        /// Called when the activity is resumed from pause
        /// </summary>
        protected override void OnResume()
        {
            _game?.Resume();
            base.OnResume();
        }

    }
}
