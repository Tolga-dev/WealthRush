using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace Managers.Controllers.Ads
{
    [Serializable]
    public class AppOpenController
    {
#if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-2624974978750920/8406648871";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string _adUnitId = "unused";
#endif
        private AppOpenAd _appOpenAd;
        private DateTime _expireTime;

        public bool IsAdAvailable => _appOpenAd != null && _appOpenAd.CanShowAd() && DateTime.Now < _expireTime;

        private void Awake()
        {
            AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        }

        private void OnDestroy()
        {
            // Always unlisten to events when complete.
            AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
        }

        private void OnAppStateChanged(AppState state)
        {
            Debug.Log("App State changed to : "+ state);

            // if the app is Foregrounded and the ad is available, show it.
            if (state == AppState.Foreground)
            {
                if (IsAdAvailable)
                {
                    ShowAppOpenAd();
                }
            }
        }
        public void ShowAppOpenAd()
        {
            if (_appOpenAd != null && _appOpenAd.CanShowAd())
            {
                Debug.Log("Showing app open ad.");
                _appOpenAd.Show();
            }
            else
            {
                Debug.LogError("App open ad is not ready yet.");
            }
        }
        
        public void LoadAppOpenAd()
        {
            if (_appOpenAd != null)
            {
                _appOpenAd.Destroy();
                _appOpenAd = null;
            }

            Debug.Log("Loading the app open ad.");

            // Create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            AppOpenAd.Load(_adUnitId, adRequest,
                (AppOpenAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("app open ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("App open ad loaded with response : "
                              + ad.GetResponseInfo());
                    _expireTime = DateTime.Now + TimeSpan.FromHours(4);
                    _appOpenAd = ad;
                    RegisterEventHandlers(ad);
                });
            
        }
        private void RegisterEventHandlers(AppOpenAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("App open ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("App open ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("App open ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("App open ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("App open ad full screen content closed.");
                LoadAppOpenAd();
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("App open ad failed to open full screen content " +
                               "with error : " + error);
                LoadAppOpenAd();
            };
            
        }
    }
}