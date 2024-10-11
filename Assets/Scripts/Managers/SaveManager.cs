using System.Collections;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        public GameManager gameManager;
        private const string PlayerNameKey = "GameSave";

        public IEnumerator Save()
        {
            
            var playerData = JsonUtility.ToJson(gameManager.gamePropertiesInSave);
            SavePlayerName(playerData);
            Debug.Log(playerData);
            Debug.Log("Game saved!");

            yield return null;
        }

        public void Load()
        {
            var playerData = LoadPlayerName();
            if (playerData == null)
            {
                StartCoroutine(Save());
                return;
            }
            Debug.Log(playerData);
            JsonUtility.FromJsonOverwrite(playerData, gameManager.gamePropertiesInSave);
            Debug.Log("Game loaded!");
        }

        private void SavePlayerName(string playerName)
        {
            PlayerPrefs.SetString(PlayerNameKey, playerName);
            PlayerPrefs.Save();
        }

        private string LoadPlayerName()
        {
            if (PlayerPrefs.HasKey(PlayerNameKey))
            {
                return PlayerPrefs.GetString(PlayerNameKey);
            }

            return null; // Default value if

        }
    }
}