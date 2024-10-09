using System;
using System.Collections;
using System.Collections.Generic;
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

        [Header("Game Sounds")]
        // music
        public AudioSource gameMusic;
        public AudioClip onMenuStateSound;
        public AudioClip onMarketSound;
        public AudioClip onGameWinSound;
        public List<AudioClip> onGameSound = new List<AudioClip>();
        // ui
        public AudioClip buttonClickSound;
        public AudioClip updateComboSound;
        public AudioClip starSound;
        
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

        public void PlayASound(AudioClip audioClip)
        {
            if(gamePropertiesInSave.isGameSoundOn == false)
                return;
            
            var tempSoundPlayer = new GameObject("TempSoundPlayer");
            var audioSource = tempSoundPlayer.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.PlayOneShot(audioClip);
            Destroy(tempSoundPlayer, audioClip.length);
        }

        public void GameMusic(AudioClip audioClip)
        {
            if (gamePropertiesInSave.isGameMusicOn)
            {
                StartCoroutine(FadeOutMusic(audioClip));
            }
            else
            {
                gameMusic.volume = 0;
            }
        }
        private IEnumerator FadeOutMusic(AudioClip audioClip)
        {
            float duration = gamePropertiesInSave.gameMusicChangeDuration; // Time in seconds to fade out
            float startVolume = gamePropertiesInSave.gameMusicStartVolume;

            while (gameMusic.volume > 0)
            {
                gameMusic.volume -= startVolume * Time.deltaTime / duration;
                yield return null;
            }
            
            StartCoroutine(FadeInMusic(audioClip));
            gameMusic.volume = 0;
        }

        private IEnumerator FadeInMusic(AudioClip audioClip)
        {
            gameMusic.clip = audioClip;
            gameMusic.Play();

            float duration = gamePropertiesInSave.gameMusicChangeDuration; // Time in seconds to fade in
            gameMusic.volume = 0;

            while (gameMusic.volume < gamePropertiesInSave.gameMusicStartVolume)
            {
                gameMusic.volume += Time.deltaTime / duration;
                yield return null;
            }

            gameMusic.volume = gamePropertiesInSave.gameMusicStartVolume;
        }

        public void ButtonClickSound()
        {
            PlayASound(buttonClickSound);
        }

    }
}