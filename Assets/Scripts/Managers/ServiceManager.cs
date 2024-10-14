using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ServiceManager : MonoBehaviour
    {
        [Header("Service Managers")]
        public AdsManager adsManager;
        public InAppPurchase inAppPurchase;
        [Header("Loading UI")]
        public TextMeshProUGUI loadingDebug;
        public GameObject loadingPanel;
        public Slider loadingSlider;
        
        public List<GameObject> startGameList = new List<GameObject>();
        private void Start()
        {
            StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            loadingSlider.value = 0.25f;
            loadingDebug.text = "Connecting to Server...";
            yield return adsManager.StartAdsServer();
            
            loadingSlider.value = 0.50f;
            loadingDebug.text = "Connecting to App...";
            yield return new WaitForSeconds(0.2f);
            
            yield return inAppPurchase.StartInAppServer();
            loadingSlider.value = 1f;
            loadingDebug.text = "Connected!";
            yield return new WaitForSeconds(0.2f);
            
            foreach (var startGame in startGameList)
            {
                startGame.gameObject.SetActive(true);
            }
            
            loadingDebug.gameObject.SetActive(false);
            loadingPanel.gameObject.SetActive(false);
 
            
            yield return null;
        }
    }
}