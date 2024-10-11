using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Managers.Controllers.Ads
{
    [Serializable]
    public class RewardedController
    {
        private string _adUnitId = "ca-app-pub-2624974978750920/6226475573";
        
        private RewardedAd _rewardedAd;
        
        public void LoadRewardedAd()
        {
            if (_rewardedAd != null)
            {
                DestroyAd();
            }

            Debug.Log("Loading rewarded ad.");

            var adRequest = new AdRequest();

            RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Rewarded ad failed to load with error: " + error);
                    return;
                }
                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Rewarded load event fired with null ad and null error.");
                    return;
                }
                _rewardedAd = ad;

                // Register to ad events to extend functionality.
                RegisterEventHandlers(ad);
            });
        }
        
        public void ShowRewardedAd()
        {
            if (_rewardedAd != null)
            {
                Debug.Log("Showing interstitial ad.");
                _rewardedAd.Show((Reward reward) => {Debug.Log("You Won!");});
            }
            else
            {
                LoadRewardedAd();
                if (_rewardedAd != null)
                {
                    Debug.Log("Showing interstitial ad.");
                    _rewardedAd.Show((Reward reward) => {Debug.Log("You Won!");});
                }
                else
                {
                    Debug.LogError("Interstitial ad is not ready yet.");
                }
            }
        }

        public void DestroyAd()
        {
            if (_rewardedAd != null)
            {
                Debug.Log("Destroying rewarded ad.");
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }

        }
        private void RegisterEventHandlers(RewardedAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Rewarded ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                LoadRewardedAd();
                Debug.Log("Rewarded ad was clicked.");
            };
            // Raised when the ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Rewarded ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Rewarded ad full screen content closed.");
                LoadRewardedAd();
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Rewarded ad failed to open full screen content with error : "
                               + error);
                LoadRewardedAd();
                
            };
        }
    }
}