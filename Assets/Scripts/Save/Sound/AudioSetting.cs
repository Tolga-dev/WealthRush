using UnityEngine;

namespace Save.Sound
{
    [CreateAssetMenu(fileName = "AudioSetting", menuName = "AudioSetting", order = 0)]
    public class AudioSetting : ScriptableObject
    {
        [SerializeField] public string audioSourceInGameName;
        [SerializeField] public float volume;
    }
}