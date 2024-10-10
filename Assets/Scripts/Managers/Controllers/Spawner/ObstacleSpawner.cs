using System;
using System.Collections.Generic;
using Save.GameObjects.Obstacle;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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

                int randomIndex = Random.Range(0, 2); // 0 = spawnPoints[0], 1 = spawnPoints[1]
                int pairedIndex =
                    randomIndex == 0 ? 3 : 2; // If 0 is chosen, pair with 3 (index 3), else pair with 2 (index 2)

                var selectedSpawnPoint1 = spawnPoints[randomIndex]; // Randomly selected spawn point
                var selectedSpawnPoint2 = spawnPoints[pairedIndex]; // Paired spawn point

                
                var spawn = obstacles[Random.Range(0, obstacles.Count)]; // You can adjust this to select a random obstacle if needed

                var created1 = Object.Instantiate(spawn, selectedSpawnPoint1, true);
                AdjustPositionToSpawnPoint(created1, selectedSpawnPoint1, spawnerManager);

                spawn = obstacles[Random.Range(0, obstacles.Count)]; // You can adjust this to select a random obstacle if needed
                var created2 = Object.Instantiate(spawn, selectedSpawnPoint2, true);
                AdjustPositionToSpawnPoint(created2, selectedSpawnPoint2, spawnerManager);

                createdObstacles.Add(created1);
                createdObstacles.Add(created2);

                road.isObjSpawned[randomIndex] = true;
                road.isObjSpawned[pairedIndex] = true;
            }
        }

        private void AdjustPositionToSpawnPoint(GameObject created, Transform spawnPoint, SpawnerManager spawnerManager)
        {
            var obstacle = created.GetComponentInChildren<Obstacle>();
            obstacle.gameManager = spawnerManager.GameManager;
            
            var position = created.transform.position;
            var spawnPosition = spawnPoint.position;

            position.x = spawnPosition.x;
            position.z = spawnPosition.z;

            created.transform.position = position;
        }


        public void ResetObstacle()
        {
            foreach (var createdObstacle in createdObstacles)
            {
                Object.Destroy(createdObstacle);
            }
            createdObstacles.Clear();
        }
    }
}