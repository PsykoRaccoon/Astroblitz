using UnityEngine;
using System;
using System.Collections.Generic;

namespace Yodo1.MAS
{
    public class Yodo1U3dBannerAdView
    {
        private static List<Yodo1U3dBannerAdView> BannerAdViews = new List<Yodo1U3dBannerAdView>();
        private readonly string indexId = (((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000) + BannerAdViews.Count) + "";

        private Yodo1U3dBannerAdSize adSize;
        private Yodo1U3dBannerAdPosition adPosition;
        private string adPlacement = string.Empty;
        private string customData = string.Empty;
        private int adPositionX = 0;
        private int adPositionY = 0;

        private Action<Yodo1U3dBannerAdView> _onAdLoadedEvent;
        private Action<Yodo1U3dBannerAdView, Yodo1U3dAdError> _onAdFailedToLoadEvent;
        private Action<Yodo1U3dBannerAdView> _onAdOpenedEvent;
        private Action<Yodo1U3dBannerAdView, Yodo1U3dAdError> _onAdFailedToOpenEvent;
        private Action<Yodo1U3dBannerAdView> _onAdClosedEvent;
        private Action<Yodo1U3dBannerAdView, Yodo1U3dAdValue> _onAdPayRevenueEvent;

        public event Action<Yodo1U3dBannerAdView> OnAdLoadedEvent
        {
            add
            {
                _onAdLoadedEvent += value;
            }
            remove
            {
                _onAdLoadedEvent -= value;
            }
        }

        public event Action<Yodo1U3dBannerAdView, Yodo1U3dAdError> OnAdFailedToLoadEvent
        {
            add
            {
                _onAdFailedToLoadEvent += value;
            }
            remove
            {
                _onAdFailedToLoadEvent -= value;
            }
        }

        public event Action<Yodo1U3dBannerAdView> OnAdOpenedEvent
        {
            add
            {
                _onAdOpenedEvent += value;
            }
            remove
            {
                _onAdOpenedEvent -= value;
            }
        }

        public event Action<Yodo1U3dBannerAdView, Yodo1U3dAdError> OnAdFailedToOpenEvent
        {
            add
            {
                _onAdFailedToOpenEvent += value;
            }
            remove
            {
                _onAdFailedToOpenEvent -= value;
            }
        }

        public event Action<Yodo1U3dBannerAdView> OnAdClosedEvent
        {
            add
            {
                _onAdClosedEvent += value;
            }
            remove
            {
                _onAdClosedEvent -= value;
            }
        }

        public event Action<Yodo1U3dBannerAdView, Yodo1U3dAdValue> OnAdPayRevenueEvent
        {
            add
            {
                _onAdPayRevenueEvent += value;
            }
            remove
            {
                _onAdPayRevenueEvent -= value;
            }
        }


        public static void CallbcksEvent(Yodo1U3dAdEvent adEvent, Yodo1U3dAdError adError, string indexId, Yodo1U3dAdValue adValue)
        {
            if (string.IsNullOrEmpty(indexId))
            {
                return;
            }

            foreach (Yodo1U3dBannerAdView bannerAdView in Yodo1U3dBannerAdView.BannerAdViews)
            {
                if (bannerAdView != null && indexId.Equals(bannerAdView.indexId))
                {
                    bannerAdView.Callbacks(adEvent, adError, adValue);
                }
            }
        }

        private void Callbacks(Yodo1U3dAdEvent adEvent, Yodo1U3dAdError adError, Yodo1U3dAdValue adValue)
        {
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdError:
                    break;
                case (Yodo1U3dAdEvent)1003:
                    Yodo1U3dMasCallback.InvokeEvent(_onAdLoadedEvent, this);
                    break;
                case (Yodo1U3dAdEvent)1004:
                    Yodo1U3dMasCallback.InvokeEvent(_onAdFailedToLoadEvent, this, adError);
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Yodo1U3dMasCallback.InvokeEvent(_onAdOpenedEvent, this);
                    break;
                case (Yodo1U3dAdEvent)1005:
                    Yodo1U3dMasCallback.InvokeEvent(_onAdFailedToOpenEvent, this, adError);
                    break;
                case Yodo1U3dAdEvent.AdClosed:
                    //Yodo1U3dMasCallback.InvokeEvent(_onBannerAdClosedEvent, this);
                    //BannerAdViews.Remove(this);
                    break;
                case Yodo1U3dAdEvent.AdPayRevenue:
                    Yodo1U3dMasCallback.InvokeEvent(_onAdPayRevenueEvent, this, adValue);
                    break;
            }
        }

        /// <summary>
        /// The default `Yodo1U3dBannerAdView` constructor, the default values as following:
        /// AdSize is `Yodo1U3dBannerAdSize.Banner`,
        /// AdPosition is `Yodo1U3dBannerAdPosition.BannerBottom | Yodo1U3dBannerAdPosition.BannerHorizontalCenter`
        /// </summary>
        public Yodo1U3dBannerAdView()
        {
            this.adSize = Yodo1U3dBannerAdSize.Banner;
            this.adPosition = Yodo1U3dBannerAdPosition.BannerBottom | Yodo1U3dBannerAdPosition.BannerHorizontalCenter;
            BannerAdViews.Add(this);
        }

        /// <summary>
        /// `Yodo1U3dBannerAdView` constructor with `Yodo1U3dBannerAdSize` and `Yodo1U3dBannerAdPosition`
        /// </summary>
        /// <param name="adSize">Bannr ad size</param>
        /// <param name="adPosition">Banner ad position</param>
        public Yodo1U3dBannerAdView(Yodo1U3dBannerAdSize adSize, Yodo1U3dBannerAdPosition adPosition)
        {
            this.adSize = adSize;
            this.adPosition = adPosition;
            BannerAdViews.Add(this);
        }

        /// <summary>
        /// Custom ad position.
        /// For greater control over where a `Yodo1U3dBannerAdView` is placed on screen than what's offered by `Yodo1U3dBannerAdPosition` values,
        /// use the `Yodo1U3dBannerAdView` constructor that has x- and y-coordinates as parameters.
        ///
        /// The top-left corner of the `Yodo1U3dBannerAdView` will be positioned at the x and y values passed to the constructor, where the origin is the top-left of the screen.
        /// </summary>
        /// <param name="adSize">Banner ad size</param>
        /// <param name="x">X-coordinates in pixels</param>
        /// <param name="y">Y-coordinates in pixels</param>
        public Yodo1U3dBannerAdView(Yodo1U3dBannerAdSize adSize, int x, int y)
        {
            this.adSize = adSize;
            this.adPositionX = x;
            this.adPositionY = y;
            BannerAdViews.Add(this);
        }

        private void BannerV2(string methodName)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdsIOS.BannerV2(methodName, this.ToJsonString());
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdsAndroid.BannerV2(methodName, this.ToJsonString());
#endif
            }
        }

        /// <summary>
        /// Load banner ads, the banner ad will be displayed automatically after loaded
        /// </summary>
        public void LoadAd()
        {

#if UNITY_EDITOR

            Yodo1EditorAds.ShowBannerAdsInEditor(indexId, (int)adPosition, (int)adSize.AdType, adPositionX, adPositionY);
#endif
#if !UNITY_EDITOR
            BannerV2("loadBannerAdV2");
#endif
        }

        /// <summary>
        /// Hide banner ads
        /// </summary>
        public void Hide()
        {
#if UNITY_EDITOR
            Yodo1EditorAds.HideBannerAdsInEditor(indexId);
#endif
#if !UNITY_EDITOR
            BannerV2("hideBannerAdV2");
#endif
        }

        /// <summary>
        /// Show banner ads
        /// </summary>
        public void Show()
        {
#if UNITY_EDITOR
            Yodo1EditorAds.ShowBannerAdsInEditor(indexId, (int)adPosition, (int)adSize.AdType, adPositionX, adPositionY);
#endif
#if !UNITY_EDITOR
            BannerV2("showBannerAdV2");
#endif

        }

        /// <summary>
        /// Destroy banner ads
        /// </summary>
        public void Destroy()
        {
#if UNITY_EDITOR
            Yodo1EditorAds.DestroyBannerAdsInEditor(indexId);
#endif
            BannerV2("destroyBannerAdV2");
            Yodo1U3dMasCallback.InvokeEvent(_onAdClosedEvent, this);
            BannerAdViews.Remove(this);
        }

        public float GetHeightInPixels()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                return Yodo1U3dAdsIOS.GetBannerHeightInPixels((int)this.adSize.AdType);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                return Yodo1U3dAdsAndroid.GetBannerHeightInPixels((int)this.adSize.AdType);
#endif
            }
            return 1.0f;
        }

        public float GetWidthInPixels()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                return Yodo1U3dAdsIOS.GetBannerWidthInPixels((int)this.adSize.AdType);
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                return Yodo1U3dAdsAndroid.GetBannerWidthInPixels((int)this.adSize.AdType);
#endif
            }
            return 1.0f;
        }

        public void SetAdPlacement(string adPlacement)
        {
            this.adPlacement = adPlacement;
        }

        public void SetCustomData(string customData)
        {
            this.customData = customData;
        }

        public string ToJsonString()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("adSize", (int)this.adSize.AdType);
            dic.Add("adPosition", (int)this.adPosition);
            dic.Add("customAdPositionX", this.adPositionX);
            dic.Add("customAdPositionY", this.adPositionY);
            dic.Add("indexId", this.indexId);
            if (string.IsNullOrEmpty(this.adPlacement))
            {
                dic.Add("adPlacement", "");
            }
            else
            {
                dic.Add("adPlacement", this.adPlacement);
            }

            if (string.IsNullOrEmpty(this.customData))
            {
                dic.Add("customData", "");
            }
            else
            {
                dic.Add("customData", this.customData);
            }
            if (_onAdPayRevenueEvent == null)
            {
                dic.Add("payRevenueEventCount", 0);
            }
            else
            {
                dic.Add("payRevenueEventCount", _onAdPayRevenueEvent.GetInvocationList().Length);
            }
            return Yodo1JSON.Serialize(dic);
        }
    }
}