using System.Collections.Generic;
using UnityEngine;

namespace Tapsell.Mediation.Show.Native
{
    internal class NativeAdViewHolder
    {
        private static NativeAdViewHolder _instance;

        private readonly Dictionary<string, NativeAdView> _adViews = new Dictionary<string, NativeAdView>();

        private NativeAdViewHolder()
        {
        }

        internal static NativeAdViewHolder Get()
        {
            if (_instance != null) return _instance;

            _instance = new NativeAdViewHolder();
            return _instance;
        }

        internal bool RegisterAdView(string adId, NativeAdView adView)
        {
            if (string.IsNullOrEmpty(adId))
            {
                Debug.LogError("[Tapsell]: NativeAdViewHolder: Cannot register adView with null or empty adId");
                return false;
            }

            if (adView == null)
            {
                Debug.LogError("[Tapsell]: NativeAdViewHolder: Cannot register null adView");
                return false;
            }

            _adViews[adId] = adView;
            return true;
        }

        internal NativeAdView GetNativeAdView(string adId)
        {
            return _adViews[adId];
        }
    }
}