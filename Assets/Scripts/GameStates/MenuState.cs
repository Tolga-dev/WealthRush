using System;
using GameStates.Base;
using UnityEngine;

namespace GameStates
{
    [Serializable]
    public class MenuState : GameState
    {
        public override void Enter()
        {
            Debug.Log("MenuState Enter");
        }

        public override void Update()
        {
            Debug.Log("MenuState Update");
        }

        public override void Exit()
        {
            Debug.Log("MenuState Exit");
        }
    }
}