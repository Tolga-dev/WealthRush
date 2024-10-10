using System;
using System.Collections;
using System.Collections.Generic;
using GameStates.Base;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameStates
{
    [Serializable]
    public class PlayingState : GameState
    {
        // static
        public Transform playerInitialPosition;
        public float startPosZ;
        
        // parameters
        public int score = 0;
        public bool isGameWon = false;
        
        [Header("Player Settings")] 
        public Transform gamePanel;
        // upper ui
        public TextMeshProUGUI scoreText;
        public List<Transform> stars = new List<Transform>();
        public Slider processSlider;
        public TextMeshProUGUI processLeftText;
        public TextMeshProUGUI processRightText;
        public Button reloadButton;

        public TextMeshProUGUI extraBonus;
        public TextMeshProUGUI extraComboBonus;
        
        public override void Init(GameManager gameManager)
        {
            base.Init(gameManager);
            
            // upper ui
            reloadButton.onClick.AddListener(() =>
            {
                GameManager.ChangeState(GameManager.playingState);
            });

            startPosZ = playerInitialPosition.transform.position.z;
        }

        public override void Enter()
        {
             
            Debug.Log("PlayingState Enter");
            ResetPlayGameUI();
            GameManager.SwitchToPlayerCam();
            GameManager.GameMusic(GameManager.onGameSound[Random.Range(0, GameManager.onGameSound.Count)]);
            
            GameManager.playerController.ResetPlayer();
            GameManager.playerController.StartRunning();
        }

        private void ResetPlayGameUI()
        {
            gamePanel.gameObject.SetActive(true);
            scoreText.text = "0";
            extraBonus.text = "";
            extraComboBonus.text = "";
            processSlider.value = 0;
            score = 0;
            isGameWon = false;
            processLeftText.text = GameManager.gamePropertiesInSave.currenLevel.ToString();
            processRightText.text = (GameManager.gamePropertiesInSave.currenLevel + 1).ToString();
            
            SetStarsTransform(false);
        }
        
        public override void Update()
        {
            if (isGameWon) return;
            
            GameManager.playerController.UpdatePlayer();

            UpdateSlider();
        }

        private void UpdateSlider()
        {
            float endPosZ = GameManager.spawnerManager.roadSpawner.createdBossRoad.transform.position.z;
            float currentPosZ = GameManager.playerController.transform.position.z;

            if (Math.Abs(endPosZ - startPosZ) > 0.1f) // Prevent division by zero
            {
                var normalizedValue = Mathf.Clamp01((currentPosZ - startPosZ) / (endPosZ - startPosZ));
                processSlider.value = normalizedValue;
            }
            else
            {
                processSlider.value = 0;
            }

            switch (processSlider.value)
            {
                case > 0.9f:
                    SetActiveStars(2);
                    break;
                case > 0.5f:
                    SetActiveStars(1);
                    break;
                case > 0.1f:
                    SetActiveStars(0);
                    break;
            }
        }

        private void SetActiveStars(int p0)
        {
            if (stars[p0].gameObject.activeSelf == false)
            {
                stars[p0].gameObject.SetActive(true);
                GameManager.PlayASound(GameManager.starSound);

                GameManager.playerController.zSpeed += p0 * 5;
            }

        }

        public override void Exit()
        {
            gamePanel.gameObject.SetActive(false);
            isGameWon = false;
            extraBonus.text = "";
            extraComboBonus.text = "";
            SetStarsTransform(false);
            GameManager.playerController.pileController.ResetPile();
            GameManager.StartCoroutine(GameManager.spawnerManager.ResetSpawners());
            GameManager.gamePropertiesInSave.money += score;
            Debug.Log("PlayingState Exit");
        }
        
        public void UpdateScore()
        {
            scoreText.text = score.ToString();
        }
        private void SetStarsTransform(bool active)
        {
            foreach (var star in stars)
            {
                star.gameObject.SetActive(active);
            }
        }
    
    }
}