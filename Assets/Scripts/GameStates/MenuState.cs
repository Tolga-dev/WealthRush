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
        
        // no ads
        public Button startNoAds;
        public Transform noAdsPanel;
        
        // money
        public TextMeshProUGUI paraAmount;
        
        // Combo
        public Button updateCombo;

        public override void Init(GameManager gameManager)
        {
            base.Init(gameManager);

            SetUI();
        }

        private void SetUI()
        {
            startSettings.onClick.AddListener(() =>
            {
                settingPanel.gameObject.SetActive(true);
            });

            startNoAds.onClick.AddListener(() =>
            {
                noAdsPanel.gameObject.SetActive(true);
            });

            updateCombo.onClick.AddListener(UpdateCombo);
        }

        public override void Enter()
        {
            menuPanel.gameObject.SetActive(true);
            paraAmount.text = GameManager.gamePropertiesInSave.money.ToString();
            Debug.Log("MenuState Enter");
        }

        public override void Update()
        {
            
            GameManager.playerController.inputController.HandleMouseInput();
            if(GameManager.playerController.inputController.canMove)
               GameManager.ChangeState(GameManager.playingState);
            
            Debug.Log("MenuState Update");
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
    }
}