using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace Managers.Controllers.Ads
{
    [Serializable]
    public class AppOpenController
    {
        private string _adUnitId = "ca-app-pub-2624974978750920/8406648871";
        private AppOpenAd _appOpenAd;

        public bool IsAdAvailable => _appOpenAd != null && _appOpenAd.CanShowAd();

        public void Awake()
        {
            AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        }

        public void OnDestroy()
        {
            // Always unlisten to events when complete.
            AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
        }

        private void OnAppStateChanged(AppState state)
        {
            Debug.Log("App State changed to : "+ state);

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
                LoadAppOpenAd();
                if (_appOpenAd != null && _appOpenAd.CanShowAd())
                {
                    _appOpenAd.Show();
                }
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

            var adRequest = new AdRequest();

            AppOpenAd.Load(_adUnitId, adRequest,
                (AppOpenAd ad, LoadAdError error) =>
                {
                    if (error != null || ad == null)
                    {
                        Debug.LogError("app open ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("App open ad loaded with response : "
                              + ad.GetResponseInfo());
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