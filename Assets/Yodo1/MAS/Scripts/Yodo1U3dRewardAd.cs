using UnityEngine;
using System;
using System.Collections.Generic;

namespace Yodo1.MAS
{
    public class Yodo1U3dRewardAd
    {
        private string adPlacement = string.Empty;
        private string customData = string.Empty;

        private Action<Yodo1U3dRewardAd> _onAdLoadedEvent;
        private Action<Yodo1U3dRewardAd, Yodo1U3dAdError> _onAdLoadFailedEvent;
        private Action<Yodo1U3dRewardAd> _onAdOpeningEvent;
        private Action<Yodo1U3dRewardAd> _onAdOpenedEvent;
        private Action<Yodo1U3dRewardAd, Yodo1U3dAdError> _onAdOpenFailedEvent;
        private Action<Yodo1U3dRewardAd> _onAdClosedEvent;
        private Action<Yodo1U3dRewardAd> _onAdEarnedEvent;
        private Action<Yodo1U3dRewardAd, Yodo1U3dAdValue> _onAdPayRevenueEvent;

        public event Action<Yodo1U3dRewardAd> OnAdLoadedEvent
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

        public event Action<Yodo1U3dRewardAd, Yodo1U3dAdError> OnAdLoadFailedEvent
        {
            add
            {
                _onAdLoadFailedEvent += value;
            }
            remove
            {
                _onAdLoadFailedEvent -= value;
            }
        }

        public event Action<Yodo1U3dRewardAd> OnAdOpeningEvent
        {
            add
            {
                _onAdOpeningEvent += value;
            }
            remove
            {
                _onAdOpeningEvent -= value;
            }
        }

        public event Action<Yodo1U3dRewardAd> OnAdOpenedEvent
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

        public event Action<Yodo1U3dRewardAd, Yodo1U3dAdError> OnAdOpenFailedEvent
        {
            add
            {
                _onAdOpenFailedEvent += value;
            }
            remove
            {
                _onAdOpenFailedEvent -= value;
            }
        }

        public event Action<Yodo1U3dRewardAd> OnAdClosedEvent
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

        public event Action<Yodo1U3dRewardAd> OnAdEarnedEvent
        {
            add
            {
                _onAdEarnedEvent += value;
            }
            remove
            {
                _onAdEarnedEvent -= value;
            }
        }

        public event Action<Yodo1U3dRewardAd, Yodo1U3dAdValue> OnAdPayRevenueEvent
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

        private static class HelperHolder
        {
            public static Yodo1U3dRewardAd Helper = new Yodo1U3dRewardAd();
        }

        public static Yodo1U3dRewardAd GetInstance()
        {
            return HelperHolder.Helper;
        }

        public static void CallbcksEvent(Yodo1U3dAdEvent adEvent, Yodo1U3dAdError adError, Yodo1U3dAdValue adValue = null)
        {
            Yodo1U3dRewardAd.GetInstance().Callbacks(adEvent, adError, adValue);
        }

        private void Callbacks(Yodo1U3dAdEvent adEvent, Yodo1U3dAdError adError, Yodo1U3dAdValue adValue)
        {
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdLoaded:
                    Yodo1U3dMasCallback.InvokeEvent(_onAdLoadedEvent, this);
                    break;
                case Yodo1U3dAdEvent.AdLoadFail:
                    Yodo1U3dMasCallback.InvokeEvent(_onAdLoadFailedEvent, this, adError);
                    break;
                case Yodo1U3dAdEvent.AdOpening:
                    Yodo1U3dMasCallback.InvokeEvent(_onAdOpeningEvent, this);
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Yodo1U3dMasCallback.Instance.Pause();
                    Yodo1U3dMasCallback.InvokeEvent(_onAdOpenedEvent, this);
                    break;
                case Yodo1U3dAdEvent.AdOpenFail:
                    Yodo1U3dMasCallback.Instance.UnPause();
                    Yodo1U3dMasCallback.InvokeEvent(_onAdOpenFailedEvent, this, adError);
                    break;
                case Yodo1U3dAdEvent.AdClosed:
                    Yodo1U3dMasCallback.Instance.UnPause();
                    Yodo1U3dMasCallback.InvokeEvent(_onAdClosedEvent, this);
                    break;
                case Yodo1U3dAdEvent.AdReward:
                    Yodo1U3dMasCallback.InvokeEvent(_onAdEarnedEvent, this);
                    break;
                case Yodo1U3dAdEvent.AdPayRevenue:
                    Yodo1U3dMasCallback.InvokeEvent(_onAdPayRevenueEvent, this, adValue);
                    break;
            }
        }

        public bool autoDelayIfLoadFail = false;

        /// <summary>
        /// The default `Yodo1U3dRewardAd` constructor
        /// </summary>
        private Yodo1U3dRewardAd()
        {

        }

        private void RewardV2(string methodName)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                Yodo1U3dAdsIOS.RewardV2(methodName, this.ToJsonString());
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                Yodo1U3dAdsAndroid.RewardV2(methodName, this.ToJsonString());
#endif
            }
        }

        private bool IsAdLoadedV2(string methodName)
        {
#if UNITY_EDITOR
            return true;
#else
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE
                return Yodo1U3dAdsIOS.IsAdLoadedV2(methodName, this.ToJsonString());
#endif
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                return Yodo1U3dAdsAndroid.IsAdLoadedV2(methodName);
#endif
            }
            return false;
#endif
        }

        [System.Obsolete("", true)]
        private void SetAdPlacement(string adPlacement)
        {
            this.adPlacement = adPlacement;
        }

        public void LoadAd()
        {
#if UNITY_EDITOR
            Yodo1EditorAds.LoadRewardedVideodsInEditor();
#endif
#if !UNITY_EDITOR
            RewardV2("loadRewardAdV2");
#endif
        }

        public bool IsLoaded()
        {
            return IsAdLoadedV2("isRewardedAdLoadedV2");
        }

        /// <summary>
        /// Show reward ads
        /// </summary>
        public void ShowAd()
        {
            ShowAd(string.Empty, string.Empty);
        }

        public void ShowAd(string placement)
        {
            ShowAd(placement, string.Empty);
        }

        public void ShowAd(string placement, string customData)
        {
            HandleOpeningEvent();
            this.adPlacement = placement;
            this.customData = customData;
#if UNITY_EDITOR
            Yodo1EditorAds.ShowRewardedVideodsInEditor();
#endif
#if !UNITY_EDITOR
            RewardV2("showRewardAdV2");
#endif
        }

        private void HandleOpeningEvent()
        {
            if (IsLoaded())
            {
                CallbcksEvent(Yodo1U3dAdEvent.AdOpening, null);
            }
        }

        public string ToJsonString()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
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
            dic.Add("autoDelayIfLoadFail", this.autoDelayIfLoadFail);
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