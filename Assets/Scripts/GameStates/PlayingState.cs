using System;
using GameStates.Base;
using UnityEngine;

namespace GameStates
{
    [Serializable]
    public class PlayingState : GameState
    {
        public override void Enter()
        {
            Debug.Log("PlayingState Enter");
        }

        public override void Update()
        {
            Debug.Log("PlayingState Update");
        }

        public override void Exit()
        {
            Debug.Log("PlayingState Exit");
        }
    }
}