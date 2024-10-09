using UnityEngine;

namespace Save.GameSo
{
    [CreateAssetMenu(fileName = "GamePropertiesInSave", menuName = "Game/GamePropertiesInSave", order = 0)]
    public class GamePropertiesInSave : ScriptableObject
    {
        public int money;
        
        public bool isGameMusicOn;
        public bool isGameSoundOn;
        
        public float gameMusicStartVolume = 0.25f;
        public float gameMusicChangeDuration = 3;
        public bool isNoAds;
        
        [Header("Level")]
        public int currenLevel;
    }
 

}