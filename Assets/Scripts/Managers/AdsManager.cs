using System;
using GoogleMobileAds.Api;
using Managers.Controllers.Ads;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    [Serializable]
    public class AdsManager : MonoBehaviour
    {
        public BannerAdsController bannerAdsController;
        public InterstitialController interstitialController;
        public RewardedController rewardedController;
        public RewardedInterstitialController rewardedInterstitialController;
        
        public void Start()
        {
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                bannerAdsController.LoadBannerView();
            });
            Debug.Log("Banner Ad Initialized");
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                interstitialController.LoadAd();
            });
            Debug.Log("Rewarded Interstitial Ad Initialized");
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                rewardedController.LoadRewardedAd();
            });
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                rewardedInterstitialController.LoadRewardedAd();
            });
            Debug.Log("Rewarded Ad Initialized");
            
        }
        public void ShowBanner()
        {
            bannerAdsController.LoadBannerView();
        }
        
        public void ShowInterstitialAd()
        {
            interstitialController.ShowAd();
        }
        
        public void ShowRewardedAd()
        {
            rewardedController.ShowRewardedAd();
        }
        
        public void ShowRewardedInterstitialAd()
        {
            rewardedInterstitialController.ShowRewardedAd();
        }



    }
}