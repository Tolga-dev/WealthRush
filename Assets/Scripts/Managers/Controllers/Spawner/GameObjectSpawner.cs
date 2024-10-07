using System;
using Save.GameObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers.Controllers.Spawner
{
    [Serializable]
    public class GameObjectSpawner
    {
        public Transform spawnPoint;
        public ObjectBase objectBase;

        public virtual GameObject SpawnObject(GameObject spawn = null)
        {
            if (spawn == null)
                spawn = objectBase.objectPrefab;
            return Object.Instantiate(spawn, spawnPoint.position, Quaternion.identity);
        }
        
    }
}