using System;
using Save.GameObjects;
using UnityEngine;

namespace Managers.Controllers.Spawner
{
    [Serializable]
    public class RoadSpawner: GameObjectSpawner
    {
        public ObjectBase bossGroundObjectBase;
        public Vector3 offset;

        public override GameObject SpawnObject(GameObject spawn = null)
        {
            SetNewPos();
            var spawnObject = base.SpawnObject(objectBase.objectPrefab);
            return spawnObject;
        }

        public GameObject SpawnBossObject()
        {
            SetNewPos();
            var spawnObject = base.SpawnObject(bossGroundObjectBase.objectPrefab);
            return spawnObject;
        }

        public Vector3 SetNewPos()
        {
            return spawnPoint.position += offset; 
        }
    }

}