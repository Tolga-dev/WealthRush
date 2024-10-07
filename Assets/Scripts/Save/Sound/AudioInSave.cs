using UnityEngine;

namespace Save.Sound
{
    [CreateAssetMenu(fileName = "AudioInSave", menuName = "AudioInSave", order = 0)]
    public class AudioInSave : ScriptableObject
    {
        [SerializeField] public string audioName;
        [SerializeField] public AudioClip audioClip;
        
    }
}