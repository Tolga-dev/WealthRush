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
        public AppOpenController appOpenController;
        public InterstitialController interstitialController;
        public RewardedInterstitialController rewardedInterstitialController;
        public RewardedController rewardedController;
        
        public void Start()
        {
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                bannerAdsController.LoadBannerView();
                appOpenController.LoadAppOpenAd();
                interstitialController.LoadAd();
                rewardedInterstitialController.LoadRewardedAd();
                rewardedController.LoadRewardedAd();
            });
            
        }
        
        public void ShowInterstitialAd()
        {
            interstitialController.ShowAd();
        }
        public void ShowRewardedInterstitialAd()
        {
            rewardedInterstitialController.ShowRewardedAd();
        }
        public void ShowRewardedAd()
        {
            rewardedController.ShowRewardedAd();
        }
    }
}