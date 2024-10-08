using System;
using Save.GameObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers.Controllers.Spawner
{
    [Serializable]
    public class RoadSpawner: GameObjectSpawner
    {
        [FormerlySerializedAs("bossGroundObjectBase")] public ObjectBaseSo bossGroundObjectBaseSo;
        public Vector3 offset;

        public override GameObject SpawnObject(GameObject spawn = null)
        {
            SetNewPos();
            var spawnObject = base.SpawnObject(objectBaseSo[0].objectPrefab);
            return spawnObject;
        }

        public GameObject SpawnBossObject()
        {
            SetNewPos();
            var spawnObject = base.SpawnObject(bossGroundObjectBaseSo.objectPrefab);
            return spawnObject;
        }

        public Vector3 SetNewPos()
        {
            return spawnPoint.position += offset; 
        }
    }

}