using System;
using System.Collections;
using GoogleMobileAds.Api;
using Managers;
using UnityEngine;

namespace Ads
{
    public class AdsManager : MonoBehaviour
    {
        
#if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-2624974978750920/2553989724";
#else
  private string _adUnitId = "unused";
#endif
        
        private RewardedAd _rewardedAd; 
        public GameManager gameManager;
        
        public void Init(GameManager gameManagerInGame)
        {
            gameManager = gameManagerInGame;
            MobileAds.Initialize(initStatus => { LoadRewardedAd();});
        }
        
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

        public void ShowRewardedAd(Action callBack)
        {
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                Debug.Log("Showing rewarded ad.");
                _rewardedAd.Show((Reward reward) =>
                {
                    StartCoroutine(WaitALittle(callBack));
                });
            }
            else
            {
                LoadRewardedAd();
                if (_rewardedAd != null && _rewardedAd.CanShowAd())
                {
                    _rewardedAd.Show((Reward reward) => { StartCoroutine(WaitALittle(callBack)); });
                }
            }
        }

        private IEnumerator WaitALittle(Action callBack)
        {
            yield return new WaitForSeconds(0.2f);
            callBack.Invoke();
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