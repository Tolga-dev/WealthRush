using System;
using Managers;
using Save.GameObjects.Prizes;

namespace GameStates.Base
{
    public class GameState
    {
        protected GameManager GameManager;
        public virtual void Init(GameManager gameManager)
        {
            GameManager = gameManager;
        }
        
        public virtual void Enter()
        {
            GameManager.spawnerManager.ResetSpawners();
        }

        public virtual void Update()
        {
            
        }
        
        public virtual void Exit()
        {
            
        }
        
    }
}