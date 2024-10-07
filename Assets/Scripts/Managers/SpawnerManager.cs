using System;
using System.Collections.Generic;
using Save.GameObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public class SpawnerManager : MonoBehaviour
    {
        public GameManager gameManager;

        public RoadSpawner roadSpawner;

        public List<Road.Road> createdRoads = new List<Road.Road>();
        
        private void Start()
        {
            RoadSpawner();
        }

        public void RoadSpawner()
        {
            for (int i = 0; i < 10; i++)
            {
                var created = roadSpawner.SpawnObject();
                createdRoads.Add(created.GetComponent<Road.Road>());
            }

            var boss = roadSpawner.SpawnBossObject();
            createdRoads.Add(boss.GetComponent<Road.Road>());
        }
        
    }
    
    
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