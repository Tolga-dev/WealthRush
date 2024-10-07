using UnityEngine;

namespace Save
{
    public class SaveManager : MonoBehaviour
    {
        public void Save()
        {
            Debug.Log("Game saved!");
        }

        public void Load()
        {
            Debug.Log("Game loaded!");
        }
    }
}