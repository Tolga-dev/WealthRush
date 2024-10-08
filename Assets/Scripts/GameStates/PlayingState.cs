using System;
using GameStates.Base;
using UnityEngine;

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