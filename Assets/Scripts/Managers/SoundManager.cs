using System;
using System.Collections.Generic;
using Core;
using Save.Sound;
using UnityEngine;

namespace Managers
{
    public class SoundManager : Singleton<SoundManager>
    {
        public GameManager gameManager;

        public List<AudioSources> audioSources = new List<AudioSources>();
        
        public void PlayOnceTime(string audioSourceName)
        {
            
        }

    }

    [Serializable]
    public class AudioSources
    {
        public string audioSourceName;
        public AudioSource audioSource;

    }
}