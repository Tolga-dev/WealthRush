using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Managers.Controllers.Ads
{
    [Serializable]
    public class BannerAdsController
    { 
        private string _adUnitId = "ca-app-pub-2624974978750920/2553989724"; // Replace with your ad unit ID
        public BannerView BannerView;

        public void LoadBannerView()
        {
            if (BannerView != null)
            {
                DestroyAd();
            }

            // Create the BannerView
            BannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);

            var adRequest = new AdRequest { };
            
            BannerView.LoadAd(adRequest);
            ListenToAdEvents();
        }

        public void ShowAd()
        {
            if (BannerView != null)
            {
                Debug.Log("Showing banner ad.");
                BannerView.Show();
            }
            else
            {
                Debug.LogError("Banner ad is not ready yet.");
            }
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
            BannerView.OnBannerAdLoaded += () =>
            {
                ShowAd();
                Debug.Log("Banner view loaded an ad with response: " + BannerView.GetResponseInfo());
            };
            BannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                LoadBannerView();
                Debug.LogError("Banner view failed to load an ad with error: " + error);
            };
            BannerView.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log($"Banner view paid {adValue.Value} {adValue.CurrencyCode}.");
            };
            BannerView.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Banner view recorded an impression.");
            };
            BannerView.OnAdClicked += () =>
            {
                LoadBannerView();
                Debug.Log("Banner view was clicked.");
            };
        }
    }
}
