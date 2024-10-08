using System;
using Core;
using GameStates;
using GameStates.Base;
using Player;
using Save;

namespace Managers
{
    [Serializable]
    public class GameManager :  Singleton<GameManager>
    {
        public GamePropertiesInSave gamePropertiesInSave;
        public GameState CurrentState;
        
        public MenuState menuState;
        public PlayingState playingState;

        public PlayerController playerController;
        public SelectorManager selectorManager;
        
        public void Start()
        {
            menuState = new MenuState();
            playingState = new PlayingState();

            ChangeState(menuState);
        }
        
        public void Update()
        {
            CurrentState.Update();
        }
        
        
        public void ChangeState(GameState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

    }
}