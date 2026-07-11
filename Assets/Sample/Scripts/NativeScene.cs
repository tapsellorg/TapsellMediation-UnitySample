using Tapsell.Mediation.Show.Native;
using UnityEngine;

namespace Sample.Scripts
{
    public class NativeScene : MonoBehaviour
    {
        private const string ZoneID = TapsellMediationKeys.NATIVE;
        private static string _adId;

        public void Request()
        {
            Tapsell.Mediation.Tapsell.RequestNativeAd(ZoneID,
                adId =>
                {
                    Debug.Log("onNativeAd requestSuccess");
                    _adId = adId;
                },
                (error) => { Debug.Log("onNativeAd requestFailed: " + error); }
            );
        }

        public void Show()
        {
            if (string.IsNullOrEmpty(_adId))
            {
                Debug.Log("onNativeAd showFailed: No ad ready. Call Request first and wait for success callback.");
                return;
            }

            var advertiser = GameObject.Find("Advertiser");
            var banner = GameObject.Find("Banner");
            var description = GameObject.Find("Description");
            var icon = GameObject.Find("Icon");
            var title = GameObject.Find("Title");
            var adChoices = GameObject.Find("AdChoices");
            var cta = GameObject.Find("CTA");

            advertiser.AddComponent<BoxCollider>();
            banner.AddComponent<BoxCollider>();
            description.AddComponent<BoxCollider>();
            icon.AddComponent<BoxCollider>();
            title.AddComponent<BoxCollider>();
            adChoices.AddComponent<BoxCollider>();
            cta.AddComponent<BoxCollider>();

            var nativeAdView = new NativeAdView.Builder()
                .WithAdvertiserText(advertiser)
                .WithBannerImage(banner)
                .WithDescriptionText(description)
                .WithIconImage(icon)
                .WithTitleText(title)
                .WithAdChoicesImage(adChoices)
                .WithCtaButton(cta)
                .Build();

            Tapsell.Mediation.Tapsell.ShowNativeAd(
                _adId,
                nativeAdView,
                () => { Debug.Log("onNativeAd impression"); },
                () => { Debug.Log("onNativeAd click"); },
                message => { Debug.Log("onNativeAd showFailed: " + message); }
            );
        }

        public void Destroy()
        {
            Tapsell.Mediation.Tapsell.DestroyNativeAd(_adId);
        }
    }
}
