using System.Collections.Generic;
using UnityEngine;

namespace Save.GameObjects.Road
{
    public class Road : MonoBehaviour
    {
        [Header("Closest to far")]
        public List<Transform> spawnPoint = new List<Transform>();
        public List<bool> isObjSpawned = new List<bool>(); // hate sorry
    }
}