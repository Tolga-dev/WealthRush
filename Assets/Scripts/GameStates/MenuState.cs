using System;
using GameStates.Base;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameStates
{
    
    [Serializable]
    public class MenuState : GameState
    {
        public Transform menuPanel;
        
        // settings
        public Button startSettings;
        public Transform settingPanel;
        public Button changeStatusMusicButton;
        public Button changeStatusSoundButton;
        
        // no ads
        public Button startMarket;
        public Button exitMarket;
        public Button buyNoAds;
        public TextMeshProUGUI description;
        public Transform marketPanel;
        
        // money
        public TextMeshProUGUI paraAmount;
        
        // Combo
        public Button updateCombo;

        public override void Init(GameManager gameManager)
        {
            base.Init(gameManager);
            SetUI();
        }
        public override void Enter()
        {
            SetMenuStateUI();

            GameManager.playerController.inputController.isMouseDown = false;
            GameManager.playerController.inputController.canMove = false;
            
            GameManager.playerController.ResetPlayer();
            GameManager.GameMusic(GameManager.onMenuStateSound);
            Debug.Log("MenuState Enter");
        }
        public override void Update()
        {
            GameManager.playerController.inputController.HandleMouseInput();
            if(GameManager.playerController.inputController.canMove)
               GameManager.ChangeState(GameManager.playingState);
        }

        public override void Exit()
        {
            menuPanel.gameObject.SetActive(false);
            Debug.Log("MenuState Exit");
        }
        public void UpdateCombo()
        {
            Debug.Log("Combo Updated");
        }
        private void SetMenuStateUI()
        {
            paraAmount.text = GameManager.gamePropertiesInSave.money.ToString();
            menuPanel.gameObject.SetActive(true);
        }
        private void SetUI()
        {
            ToggleButtonPosition(changeStatusMusicButton, GameManager.gamePropertiesInSave.isGameMusicOn);
            ToggleButtonPosition(changeStatusSoundButton, GameManager.gamePropertiesInSave.isGameSoundOn);
            if (GameManager.gamePropertiesInSave.isNoAds)
            {
                buyNoAds.gameObject.SetActive(false);
                description.text = "You have already bought No Ads!";
            }
            else
            {
                description.text = "Remove ads with 2$"; // money might be changed
            }
            
            updateCombo.onClick.AddListener(() =>
            {
                UpdateCombo();
                GameManager.ButtonClickSound();
                GameManager.PlayASound(GameManager.updateComboSound);
            });
            
            changeStatusMusicButton.onClick.AddListener(() =>
            {
                GameManager.ButtonClickSound();
                
                if (GameManager.gamePropertiesInSave.isGameMusicOn)
                {
                    GameManager.GameMusic(null);
                    GameManager.gamePropertiesInSave.isGameMusicOn = false;
                }
                else
                {
                    GameManager.gamePropertiesInSave.isGameMusicOn = true;
                    GameManager.GameMusic(GameManager.onMenuStateSound);
                }
                
                ToggleButtonPosition(changeStatusMusicButton, GameManager.gamePropertiesInSave.isGameMusicOn);
                
            });
            changeStatusSoundButton.onClick.AddListener(() =>
            {
                GameManager.ButtonClickSound();
                GameManager.gamePropertiesInSave.isGameSoundOn = !GameManager.gamePropertiesInSave.isGameSoundOn;
                ToggleButtonPosition(changeStatusSoundButton, GameManager.gamePropertiesInSave.isGameSoundOn);
            });
            
            startSettings.onClick.AddListener(() =>
            {
                settingPanel.gameObject.SetActive(true);
                GameManager.ButtonClickSound();
            });

            startMarket.onClick.AddListener(() =>
            {
                marketPanel.gameObject.SetActive(true);
                GameManager.ButtonClickSound();
                GameManager.GameMusic(GameManager.onMarketSound);
            });

            exitMarket.onClick.AddListener(() =>
            {
                GameManager.ButtonClickSound();
                marketPanel.gameObject.SetActive(false);
                GameManager.GameMusic(GameManager.onMenuStateSound);
            });
            
            exitMarket.onClick.AddListener(() =>
            {
                GameManager.ButtonClickSound();
            });
            buyNoAds.onClick.AddListener(() =>
            {
                GameManager.ButtonClickSound();
                GameManager.gamePropertiesInSave.isNoAds = true;
            });
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
    }
}