using System;
using GameStates.Base;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameStates
{
    
    [Serializable]
    public class MenuState : GameState
    {
        public Transform menuPanel;
        
        // settings
        [Header("Setting UI")]
        public Button startSettings;
        public Button exitSettings;
        public Transform settingPanel;
        public Button changeStatusMusicButton;
        public Button changeStatusSoundButton;
        
        // no ads
        [Header("Shop UI")]
        public Button startMarket;
        public Button exitMarket;
        public Button buyNoAds;
        public TextMeshProUGUI description;
        public TextMeshProUGUI shopResult;
        public Transform marketPanel;
        public Animator marketMenuAnimator;
        public Animator settingMenuAnimator;
        private static readonly int Pop = Animator.StringToHash("Pop");
        public AudioClip failedClickSound;
        // money
        [Header("Main Menu UI")]
        public TextMeshProUGUI paraAmount;
        
        // Combo
        [Header("Main Menu Combo UI")]
        public Button updateCombo;  
        public TextMeshProUGUI comboAmount;
        public TextMeshProUGUI priceAmount;
        
        // in scene game play
        [Header("Main Menu Starter")]
        public TextMeshProUGUI clickToStart;
        
        public override void Init(GameManager gameManager)
        {
            base.Init(gameManager);
            SetUI();
        }
        public override void Enter()
        {
            SetMenuStateUI();
            GameManager.SwitchToMenuStateCam();
            GameManager.playerController.ResetPlayer();
            
            GameManager.GameMusic(GameManager.onMenuStateSound);
            Debug.Log("MenuState Enter");
        }

        public override void Update()
        {
            GameManager.playerController.inputController.HandleMouseInput();
            if (GameManager.playerController.inputController.isMouseDown)
            { 
                GameManager.serviceManager.adsManager.PlaySceneTransitionAds();
               GameManager.ChangeState(GameManager.playingState);
            }
        }

        public override void Exit()
        {
            menuPanel.gameObject.SetActive(false);
            Debug.Log("MenuState Exit");
        }
      
        public void SetMenuStateUI()
        {
            comboAmount.text = "x" + (float)GameManager.gamePropertiesInSave.comboRank/10 + "+";
            priceAmount.text = GameManager.gamePropertiesInSave.price + "$";
            
            paraAmount.text = GameManager.gamePropertiesInSave.money + "$";
            menuPanel.gameObject.SetActive(true);
        }
        
        private void SetUI()
        {
            SetSaveUI();
            
            // settings
            changeStatusMusicButton.onClick.AddListener(() =>
            {
                GameManager.ButtonClickSound();

                var save = GameManager.gamePropertiesInSave;
                
                if (save.isGameMusicOn)
                {
                    GameManager.GameMusic(null);
                    save.isGameMusicOn = false;
                }
                else
                {
                    save.isGameMusicOn = true;
                    GameManager.GameMusic(GameManager.onMenuStateSound);
                }
                
                ToggleButtonPosition(changeStatusMusicButton, save.isGameMusicOn);
                
            });
            changeStatusSoundButton.onClick.AddListener(() =>
            {
                var save = GameManager.gamePropertiesInSave;

                GameManager.ButtonClickSound();
                save.isGameSoundOn = !save.isGameSoundOn;
                ToggleButtonPosition(changeStatusSoundButton, save.isGameSoundOn);
            });
            startSettings.onClick.AddListener(() =>
            {
                settingPanel.gameObject.SetActive(true);
                settingMenuAnimator.SetTrigger(Pop);
                GameManager.ButtonClickSound();
            }); 
            exitSettings.onClick.AddListener(() =>
            {
                GameManager.ButtonClickSound();
                settingMenuAnimator.SetTrigger(Pop); 
            });
            
            // combo
            updateCombo.onClick.AddListener(() =>
            {
                UpdateCombo();
                GameManager.ButtonClickSound();
            });
            
            
            // market
            startMarket.onClick.AddListener(() =>
            {
                marketPanel.gameObject.SetActive(true);
                marketMenuAnimator.SetTrigger(Pop);
                GameManager.ButtonClickSound();
                GameManager.GameMusic(GameManager.onMarketSound);
            });

            exitMarket.onClick.AddListener(() =>
            {
                GameManager.ButtonClickSound();
                marketMenuAnimator.SetTrigger(Pop); 
                GameManager.GameMusic(GameManager.onMenuStateSound);
            });
            exitMarket.onClick.AddListener(() =>
            {
                GameManager.ButtonClickSound();
                shopResult.text = "";
            });
            buyNoAds.onClick.AddListener(() =>
            {
                var success = new UnityAction<string>((string result) =>
                {
                    GameManager.ButtonClickSound();
                    GameManager.gamePropertiesInSave.isNoAds = true;
                    SetShopUI();
                    GameManager.serviceManager.adsManager.CleanUp();
                    shopResult.text = result;
                });
                
                var failed = new UnityAction<string>((string result) =>
                {
                    GameManager.PlayASound(failedClickSound);
                    shopResult.text = result;
                });
                
                GameManager.serviceManager.inAppPurchase.BuyItem(GameManager.gamePropertiesInSave.noAdsProductId,success,failed);
                
            });
            
        }

        private void SetSaveUI()
        {
            var save = GameManager.gamePropertiesInSave;
            
            ToggleButtonPosition(changeStatusMusicButton, save.isGameMusicOn);
            ToggleButtonPosition(changeStatusSoundButton, save.isGameSoundOn);
            
            // shop
            SetShopUI();
            
            // combo
            SetMenuStateUI();
        }

        private void SetShopUI()
        {
            var save = GameManager.gamePropertiesInSave;
            if (save.isNoAds)
            {
                buyNoAds.gameObject.SetActive(false);
                description.text = "You have bought No Ads!";
            }
            else
            {
                description.text = $"Remove ads with {GameManager.serviceManager.inAppPurchase.ReturnLocalizedPrice(GameManager.gamePropertiesInSave.noAdsProductId)}"; // money might be changed
            }
        }

        private void ToggleButtonPosition(Button button, bool isOn)
        {
            var rectTransform = button.GetComponent<RectTransform>();
            var width = rectTransform.rect.width;
            var vector2 = rectTransform.anchoredPosition;
            
            if (isOn)
            {
                vector2.x = 0;
            }
            else
            {
                vector2.x = width;
            }
            rectTransform.anchoredPosition = vector2;
        }

        public void UpdateCombo()
        {
            var save = GameManager.gamePropertiesInSave;
            var money = save.money;
            var price = save.price;

            if (save.isNewPriceCalculated == false)
            {
                var priceMinIncreaseAmount = save.priceMinIncreaseAmount * save.priceLevel; // Minimum price increase is 10 * priceLevel
                var priceMaxIncreaseAmount = save.priceMaxIncreaseAmount * save.priceLevel; // Minimum price increase is 10 * priceLevel

                save.newAdditionalPrice = Random.Range(priceMinIncreaseAmount, priceMaxIncreaseAmount);

                save.isNewPriceCalculated = true;
            }
            
            price += save.newAdditionalPrice;
            
            if (money >= price && save.isNewPriceCalculated)
            {
                save.money -= price;
                save.price = price;
                
                save.comboRank += (save.increaseComboAmount)*(Random.Range(1, 4));
                save.priceLevel++;

                save.isNewPriceCalculated = false;
                GameManager.PlayASound(GameManager.updateComboSound);
                GameManager.StartCoroutine(GameManager.saveManager.Save());
                GameManager.serviceManager.adsManager.PlayComboTransitionAds();
                
                SetMenuStateUI();
            }
            else
            {
                GameManager.PlayASound(GameManager.notEnoughMoney);
            }
        }

    }
}