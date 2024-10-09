using System;
using Core;
using GameStates;
using GameStates.Base;
using Player;
using Save;
using Save.GameSo;
using UnityEngine;
using UnityEngine.Serialization;

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
        public SelectorController selectorManager;
        public SpawnerManager spawnerManager;
        
        [Header("Road Borders")]
        public Transform targetA; // First target position
        public Transform targetB; // Second target position

        [Header("Level")]
        public int currenLevel;
        public void Start()
        {
            menuState.Init(this);
            playingState.Init(this);
            selectorManager.Start();
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

        public void SoundPlayer(AudioClip audioClip)
        {
            var tempSoundPlayer = new GameObject("TempSoundPlayer");
            var audioSource = tempSoundPlayer.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.PlayOneShot(audioClip);
            Destroy(tempSoundPlayer, audioClip.length);
        }

    
    }
}