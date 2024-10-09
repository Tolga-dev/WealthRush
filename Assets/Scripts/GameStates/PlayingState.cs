using System;
using GameStates.Base;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameStates
{
    [Serializable]
    public class PlayingState : GameState
    {
        public Transform playerInitialPosition;
        public bool playerWon = false;
        
        public override void Enter()
        {
            Debug.Log("PlayingState Enter");
            GameManager.GameMusic(GameManager.onGameSound[Random.Range(0, GameManager.onGameSound.Count)]);
            
            GameManager.playerController.ResetPlayer();
            GameManager.playerController.StartRunning();
        }

        public override void Update()
        {
            if (playerWon) return;
            
            GameManager.playerController.UpdatePlayer();
        }

        public override void Exit()
        {
            playerWon = false;
            Debug.Log("PlayingState Exit");
        }
    }
}