using System;
using GoogleMobileAds.Api;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers.Controllers.Ads
{
    [Serializable]
    public class BannerAdsController
    {
        
        private string _adUnitId = "ca-app-pub-2624974978750920/2553989724";
        public BannerView BannerView;

        public void LoadBannerView()
        {
            if (BannerView != null)
            {
                DestroyAd();
            }
            
            BannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);

            var adRequest = new AdRequest();
            BannerView.LoadAd(adRequest);
        }
        
        public void DestroyAd()
        {
            if (BannerView != null)
            {
                Debug.Log("Destroying banner view.");
                BannerView.Destroy();
                BannerView = null;
            }
        }
        
        private void ListenToAdEvents()
        {
            // Raised when an ad is loaded into the banner view.
            BannerView.OnBannerAdLoaded += () =>
            {
                Debug.Log("Banner view loaded an ad with response : "
                          + BannerView.GetResponseInfo());
            };
            // Raised when an ad fails to load into the banner view.
            BannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : "
                               + error);
            };
            // Raised when the ad is estimated to have earned money.
            BannerView.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            BannerView.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Banner view recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            BannerView.OnAdClicked += () =>
            {
                Debug.Log("Banner view was clicked.");
            };
            // Raised when an ad opened full screen content.
            BannerView.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Banner view full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            BannerView.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Banner view full screen content closed.");
            };
        }
    }
}