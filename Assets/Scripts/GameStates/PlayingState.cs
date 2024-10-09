using System;
using System.Collections.Generic;
using GameStates.Base;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameStates
{
    [Serializable]
    public class PlayingState : GameState
    {
        public Transform playerInitialPosition;
        public bool playerWon = false;
        public int score = 0;
        
        [Header("Player Settings")] 
        public Transform gamePanel;
        // upper ui
        public Button reloadButton;
        public TextMeshProUGUI scoreText;
        public List<Transform> stars = new List<Transform>();
        public Slider processSlider;
        public float startPosZ;
        
        public override void Init(GameManager gameManager)
        {
            base.Init(gameManager);
            
            // upper ui
            reloadButton.onClick.AddListener(() =>
            {
                Debug.Log("Reloading game");
            });
            scoreText.text = "0";


            startPosZ = playerInitialPosition.transform.position.z;
        }
        public override void Enter()
        {
            gamePanel.gameObject.SetActive(true);
            score = 0;
            
            Debug.Log("PlayingState Enter");
            GameManager.GameMusic(GameManager.onGameSound[Random.Range(0, GameManager.onGameSound.Count)]);
            
            GameManager.playerController.ResetPlayer();
            GameManager.playerController.StartRunning();
        }

        public override void Update()
        {
            if (playerWon) return;
            
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
            }

        }

        public override void Exit()
        {
            playerWon = false;
            foreach (var star in stars)
            {
                star.gameObject.SetActive(false);
            }
            Debug.Log("PlayingState Exit");
        }
        public void UpdateScore()
        {
            scoreText.text = score.ToString();
        }
        
    }
}