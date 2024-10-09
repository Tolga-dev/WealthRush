using UnityEngine;
using UnityEngine.Serialization;

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
        
        public int chestSpawnCount = 0; 
        public int maxChestSpawns = 3;
        
        [Header("Level")]
        public int currenLevel;

        public int comboRank;
        public int increaseComboAmount;

        public bool isNewPriceCalculated;
        public int price; // 100
        public int priceLevel; // start with 1
        public int priceMinIncreaseAmount ; // 10 * priceLevel
        public int priceMaxIncreaseAmount ; // 50 * priceLevel

        public int newAdditionalPrice;
        public void ResetThis()
        {
            chestSpawnCount = 0;
            newAdditionalPrice = 1;
            price = 100;
            priceLevel = 1;
        }
    }
 

}