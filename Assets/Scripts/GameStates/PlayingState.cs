using System;
using GameStates.Base;
using UnityEngine;

namespace GameStates
{
    [Serializable]
    public class PlayingState : GameState
    {
        public Transform playerInitialPosition;
        
        public override void Enter()
        {
            Debug.Log("PlayingState Enter");

            GameManager.playerController.ResetPlayer();
            GameManager.playerController.StartRunning();
        }

        public override void Update()
        {
            GameManager.playerController.UpdatePlayer();
            Debug.Log("PlayingState Update");
        }

        public override void Exit()
        {
            Debug.Log("PlayingState Exit");
        }
    }
}