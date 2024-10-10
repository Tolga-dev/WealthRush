using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Core;
using GameStates;
using GameStates.Base;
using Player;
using Save;
using Save.GameSo;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Managers
{
    [Serializable]
    public class GameManager : Singleton<GameManager>
    {
        [Header("Game Save")]
        public GamePropertiesInSave gamePropertiesInSave;
        public GameState CurrentState;

        [Header("Game States")]
        public MenuState menuState;
        public PlayingState playingState;

        [Header("Game Controllers")]
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
        [Header("UI Game Sounds")]
        public AudioClip buttonClickSound;
        public AudioClip updateComboSound;
        public AudioClip starSound;
        public AudioClip notEnoughMoney;

        // cams
        [Header("Game Cams")]
        public CinemachineVirtualCamera playerCam;
        public CinemachineVirtualCamera menuStateCam;
        public CinemachineVirtualCamera winCam;

        private CinemachineVirtualCamera _activeCam;
        public CinemachineBrain cinemaMachineBrain;

        public void Start()
        {
            menuState.Init(this);
            playingState.Init(this);
            selectorManager.Start();

            _activeCam = playerCam;
            ChangeState(menuState);
            gamePropertiesInSave.ResetThis();
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
            if (gamePropertiesInSave.isGameSoundOn == false)
                return;

            var tempSoundPlayer = new GameObject("TempSoundPlayer");
            var audioSource = tempSoundPlayer.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.volume = gamePropertiesInSave.gameSoundVolume;
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

        private void SwitchCam(CinemachineVirtualCamera newActiveCam)
        {
            if (_activeCam == newActiveCam) return;

            playerCam.Priority = 0;
            menuStateCam.Priority = 0;
            winCam.Priority = 0;

            newActiveCam.Priority = 10;
            _activeCam = newActiveCam;
        }

        public void SwitchToPlayerCam()
        {
            SwitchCam(playerCam);
        }

        public void SwitchToMenuStateCam()
        {
            SwitchCam(menuStateCam);
        }

        public void SwitchToWinCam()
        {
            SwitchCam(winCam);
        }
        
        public void OnCameraSwitch(ICinemachineCamera toCam, ICinemachineCamera fromCam)
        {
            Debug.Log("worked");
            var firstCam = (CinemachineVirtualCamera)fromCam;
            var secondCam = (CinemachineVirtualCamera)toCam;
            Debug.Log(firstCam.gameObject.name); // menu
            Debug.Log(secondCam.gameObject.name); // win
            if (firstCam == winCam && secondCam == menuStateCam)
            {
                menuState.clickToStart.text = gamePropertiesInSave.winTexts[Random.Range(0, gamePropertiesInSave.winTexts.Length)];
                playingState.clickAvoid.SetActive(true);
                StartCoroutine(WaitForCameraBlendToFinish());
            }
        }

        private IEnumerator WaitForCameraBlendToFinish()
        {
            while (cinemaMachineBrain.IsBlending)
            {
                yield return null;  // Wait until the next frame
            }

            playingState.clickAvoid.SetActive(false);
            menuState.clickToStart.text = "Tap To Play!";
            Debug.Log("Arrived at menu state!");
        }
    }
}