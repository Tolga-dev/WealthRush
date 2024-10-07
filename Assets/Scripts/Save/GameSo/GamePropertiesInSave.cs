using System;
using System.Collections.Generic;
using Save.Sound;
using UnityEngine;

namespace Save
{
    [CreateAssetMenu(fileName = "GamePropertiesInSave", menuName = "Game/GamePropertiesInSave", order = 0)]
    public class GamePropertiesInSave : ScriptableObject
    {
        public AudiosInSave audiosInSave;
    }

    [Serializable]
    public class AudiosInSave
    {
        [SerializeField] public List<AudioInSave> audioInSaves = new List<AudioInSave>();
        
        public AudioInSave GetAudioInSave(string audioName)
        {
            return audioInSaves.Find(audioInSave => audioInSave.audioName == audioName);
        }
    }

 
}