using Android.OS;
using FbonizziMonogame;
using FlowsoftGamesAndroidToolkit;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace FlowsoftGamesAndroid
{
    public abstract class FbonizziMonoGameActivity : AndroidGameActivity
    {
        private IFbonizziGame _game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            this.SetGameOptions();
            _game = StartGame(GameCultureProvider.GetCurrentCulture());
            _game.ExitGameRequested += Game_ExitGameRequested;
        }

        private void Game_ExitGameRequested(object sender, System.EventArgs e)
            => Game.Activity.MoveTaskToBack(true);

        protected abstract IFbonizziGame StartGame(CultureInfo cultureInfo);

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            if (hasFocus)
            {
                // Per fixare il fatto che con OREO il long press
                // del power button deformi il layout
                // ...buon candidato per finire nella mia : Activity
                this.SetGameOptions();
            }

            base.OnWindowFocusChanged(hasFocus);
        }

        protected override void OnDestroy()
        {
            DisposeGame();
            _game = null;
            base.OnDestroy();
        }

        protected abstract void DisposeGame();
        protected virtual void PauseAd() { }
        protected virtual void ResumeAd() { }
        
        protected override void OnPause()
        {
            PauseAd();
            _game?.Pause();
            base.OnPause();
        }

        protected override void OnResume()
        {
            ResumeAd();
            _game?.Resume();
            base.OnResume();
        }

    }
}
