using FbonizziMonoGame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Globalization;

namespace FbonizziMonoGame.PlatformStore
{
    public class RateMeDialog
    {
        private const string _titleTextKey = "RateMe|TitleTextKey";
        private const string _messageTextKey = "RateMe|MessageTextKey";
        private const string _rateItButtonTextKey = "RateMe|RateItButtonTextKey";
        private const string _notNowButtonTextKey = "RateMe|NoThanksButtonTextKey";

        private const string _dontShowAgainSettingKey = "RateMe|DontShowAgain";
        private const string _appLaunchCountSettingKey = "RateMe|AppLaunchCount";
        private const string _dateFirstLaunchSettingKey = "RateMe|DateFirstLaunch";
        private const string _rateMeLaunchCountSettingKey = "RateMe|RateMeLaunchCount";

        private ISettingsRepository _settingsRepository;
        private readonly int _launchesUntilPrompt;
        private readonly int _maxRateShowTimes;
        private readonly string _appName;
        private readonly Uri _rateAppUri;

        public bool ShouldShowDialog { get; private set; }

        private readonly Dialog _titleButtonButtonDialog;

        public RateMeDialog(
            int launchesUntilPrompt,
            int maxRateShowTimes,
            string appName,
            Uri rateAppUri,
            CultureInfo currentCulture,
            Rectangle dialogDefinition,
            SpriteFont font,
            IWebPageOpener webPageOpener,
            ISettingsRepository settingsRepository,
            Rectangle buttonADefinition,
            Rectangle buttonBDefinition,
            Color backgroundColor,
            Color buttonsBackgroundColor,
            Color buttonsShadowColor,
            Color backgroundShadowColor,
            Color titleColor,
            Color buttonsTextColor,
            Vector2 titlePositionOffset,
            float buttonTextPadding,
            float titlePadding)
        {
            _launchesUntilPrompt = launchesUntilPrompt;
            _maxRateShowTimes = maxRateShowTimes;
            _appName = appName;
            _rateAppUri = rateAppUri;
            AddStrings(currentCulture);

            _settingsRepository = settingsRepository;

            var buttonA = new ButtonWithText(
                font: font,
                text: MultilanguageStrings.Get(_rateItButtonTextKey),
                collisionRectangle: buttonADefinition,
                backgroundColor: buttonsBackgroundColor,
                textColor: buttonsTextColor,
                shadowColor: buttonsShadowColor,
                onClick: () =>
                {
                    _settingsRepository.SetBool(_dontShowAgainSettingKey, true);
                    ShouldShowDialog = false;
                    webPageOpener.OpenWebpage(rateAppUri);
                },
                textPadding: buttonTextPadding);

            var buttonB = new ButtonWithText(
                font: font,
                text: MultilanguageStrings.Get(_notNowButtonTextKey),
                collisionRectangle: buttonBDefinition,
                backgroundColor: buttonsBackgroundColor,
                textColor: buttonsTextColor,
                shadowColor: buttonsShadowColor,
                onClick: () =>
                {
                    // If users says "Not now", I reset the app launch count
                    _settingsRepository.SetInt(_appLaunchCountSettingKey, 0);
                    ShouldShowDialog = false;
                },
                textPadding: buttonTextPadding);

            var minScale = Math.Min(buttonA.TextScale, buttonB.TextScale);
            buttonA.TextScale = minScale;
            buttonB.TextScale = minScale;

            _titleButtonButtonDialog = new Dialog(
                title: MultilanguageStrings.Get(_messageTextKey),
                font: font,
                dialogWindowDefinition: dialogDefinition,
                titlePositionOffset: titlePositionOffset,
                backgroundColor: backgroundColor,
                backgroundShadowColor: backgroundShadowColor,
                titleColor: titleColor,
                titlePadding: titlePadding,
                buttons: new ButtonWithText[] { buttonA, buttonB });

            EvaluateRateMe();
        }

        public void HandleInput(Vector2 inputPosition)
            => _titleButtonButtonDialog.HandleInput(inputPosition);

        private void EvaluateRateMe()
        {
            if (_settingsRepository.GetOrSetBool(_dontShowAgainSettingKey, false))
                return;

            int appLaunchCount = _settingsRepository.GetOrSetInt(_appLaunchCountSettingKey, 0) + 1;
            _settingsRepository.SetInt(_appLaunchCountSettingKey, appLaunchCount);

            int rateMeLaunchCount = _settingsRepository.GetOrSetInt(_rateMeLaunchCountSettingKey, 0);
                      
            if (rateMeLaunchCount < _maxRateShowTimes)
            {
                if (appLaunchCount >= _launchesUntilPrompt)
                {
                    ShouldShowDialog = true;
                    _settingsRepository.SetInt(_rateMeLaunchCountSettingKey, rateMeLaunchCount + 1);
                }
            }
            else
            {
                // After n times that the dialog has been shown, It is never shown anymore.
                _settingsRepository.SetBool(_dontShowAgainSettingKey, true);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
            => _titleButtonButtonDialog.Draw(spriteBatch);

        private void AddStrings(CultureInfo currentCulture)
        {
            if (MultilanguageStrings.IsItalian(currentCulture))
            {
                MultilanguageStrings.Add(_titleTextKey, $"Valuta {_appName}");
                MultilanguageStrings.Add(_messageTextKey, $"Ti piace {_appName}?\nLascia una recensione!\nGrazie!");
                MultilanguageStrings.Add(_rateItButtonTextKey, "Ok!");
                MultilanguageStrings.Add(_notNowButtonTextKey, "Non ora");
            }
            else
            {
                MultilanguageStrings.Add(_titleTextKey, $"Rate {_appName}");
                MultilanguageStrings.Add(_messageTextKey, $"Enjoy {_appName}?\nRate it, please!\nThanks!");
                MultilanguageStrings.Add(_rateItButtonTextKey, "Rate it!");
                MultilanguageStrings.Add(_notNowButtonTextKey, "Not now");
            }
        }
    }
}
