using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers.Controllers.Spawner
{
    [Serializable]
    public class ObstacleSpawner
    {
        public List<GameObject> obstacles = new List<GameObject>();
        
        public List<GameObject> createdObstacles = new List<GameObject>();

        public virtual void SpawnObject(SpawnerManager spawnerManager)
        {
            var createdRoads = spawnerManager.roadSpawner.createdRoads;

            foreach (var road in createdRoads)
            {
                var spawnPoints = road.spawnPoint;
                var spawnPoint = spawnPoints[0];
                var spawn = obstacles[0];
                
                var created = Object.Instantiate(spawn, spawnPoint, true);
                var position = created.transform.position;
                var position1 = spawnPoint.position;
                
                position.x = position1.x;
                position.z = position1.z;

                created.transform.position = position;

                createdObstacles.Add(created);
            }
        }
        
    }    
}