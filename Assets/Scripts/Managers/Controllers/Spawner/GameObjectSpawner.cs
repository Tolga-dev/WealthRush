using System;
using System.Collections.Generic;
using Save.GameObjects;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Managers.Controllers.Spawner
{
    [Serializable]
    public class GameObjectSpawner
    {
        public Transform spawnPoint;
        public List<ObjectBaseSo> objectBaseSo = new List<ObjectBaseSo>();

        public virtual GameObject SpawnObject(GameObject spawn = null)
        {
            if (spawn == null)
                spawn = objectBaseSo[0].objectPrefab;
            return Object.Instantiate(spawn, spawnPoint.position, Quaternion.identity);
        }
        
        
    }
}