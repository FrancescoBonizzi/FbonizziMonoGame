namespace FbonizziMonoGame.AfterGameManagement
{
    public class AfterGameManager
    {
        public enum AfterGameStates
        {
            ShowAd,
            ShowProTip,
            Nothing
        }

        private int _afterGameCount = 0;
        private int? _numberOfAfterGamesToShowAd = 4;
        private readonly bool _enableProTips;

        public AfterGameManager(
            bool enableProTips,
            int? showAdsAfterNGames = null)
        {
            _enableProTips = enableProTips;
            _numberOfAfterGamesToShowAd = showAdsAfterNGames;
        }

        public AfterGameStates GetAfterGameState()
        {
            var stateToReturn = AfterGameStates.Nothing;
            if (_numberOfAfterGamesToShowAd != null &&
                (_afterGameCount % _numberOfAfterGamesToShowAd == 0))
            {
                stateToReturn = AfterGameStates.ShowAd;
            }
            else if (_enableProTips)
            {
                stateToReturn = AfterGameStates.ShowProTip;
            }

            ++_afterGameCount;
            return stateToReturn;
        }
    }
}
