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
        
        public GameManager gameManager;
        public void Start()
        {
            if(gameManager.gamePropertiesInSave.isNoAds)
                return;
            
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
            
            /*MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                rewardedInterstitialController.LoadRewardedAd();
            });*/
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
           // rewardedInterstitialController.ShowRewardedAd();
        }
        
        public bool CanPlayAds()
        {
            if (gameManager.gamePropertiesInSave.isNoAds)
                return false;
            return true;
        }

        public void PlayComboTransitionAds()
        {
            var save = gameManager.gamePropertiesInSave;

            if (CanPlayAds() == false)
                return;
            
            if (save.lastTimeComboAdWatched >= save.maxTimeBetweenComboAds)
            {
                save.lastTimeComboAdWatched = 0;
                ShowInterstitialAd();
            }
            else
            {
                save.lastTimeComboAdWatched++;
            }
        }
        public void PlaySceneTransitionAds()
        {
            var save = gameManager.gamePropertiesInSave;
            
            if (CanPlayAds() == false)
                return;
            
            if (save.lastTimeNextLevelAdWatched >= save.maxTimeBetweenNextLevel)
            {
                save.lastTimeNextLevelAdWatched = 0;
                ShowRewardedAd();
            }
            else
            {
                save.lastTimeNextLevelAdWatched++;
            }
        }
        public void CleanUp()
        {
            bannerAdsController.DestroyAd();
            interstitialController.DestroyAd();
            rewardedController.DestroyAd();
            rewardedInterstitialController.DestroyAd();
        }
        
    }
}