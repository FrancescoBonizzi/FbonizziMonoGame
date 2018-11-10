using Android.Content;
using Android.Gms.Ads;

namespace FlowsoftGamesAndroidToolkit
{
    public class SimpleInterstitialAdManager : AdListener
    {
        private readonly InterstitialAd _ad;

        public SimpleInterstitialAdManager(
            Context context,
            string adUnitId)
        {
            _ad = new InterstitialAd(context)
            {
                AdUnitId = adUnitId,
                AdListener = this
            };

            RequestNewAd();
        }

        public override void OnAdClosed()
            => RequestNewAd();

        public void RequestNewAd()
        {
            var adRequest = new AdRequest.Builder().Build();
            _ad.LoadAd(adRequest);
        }

        public bool IsLoaded
            => _ad.IsLoaded;

        public void Show()
            => _ad.Show();
    }
}
