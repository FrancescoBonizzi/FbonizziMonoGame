using Android.Gms.Ads;

namespace FlowsoftGamesAndroid
{
    public class BannerAdListener : AdListener
    {
        private readonly AdView _banner;

        public BannerAdListener(
            AdView banner)
        {
            _banner = banner;
        }

        public override void OnAdLoaded()
        {
            _banner.BringToFront();
            base.OnAdLoaded();
        }
    }
}
