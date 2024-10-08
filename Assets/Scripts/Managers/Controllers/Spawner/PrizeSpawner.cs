using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Controllers.Spawner
{
    [Serializable]
    public class PrizeSpawner
    {
        public List<GameObject> prizes = new List<GameObject>();
        public List<GameObject> createdPrizes = new List<GameObject>();

        public void SpawnObject(SpawnerManager spawnerManager)
        {
            var createdRoads = spawnerManager.roadSpawner.createdRoads;
            var createdCircleRoads = spawnerManager.roadSpawner.createdCircleRoads;

            foreach (var road in createdRoads)
            {
                for (int i = 0; i < road.isObjSpawned.Count; i++)
                {
                    var isSpawned = road.isObjSpawned[i];

                    if (isSpawned == false)
                    {
                        var spawnPoints = road.spawnPoint;

                        var selectedSpawnPoint = spawnPoints[i];

                        var spawn = prizes[0]; // You can adjust this to select a random prize if needed

                        var created = UnityEngine.Object.Instantiate(spawn, selectedSpawnPoint, true);
                        AdjustPositionToSpawnPoint(created, selectedSpawnPoint);

                        createdPrizes.Add(created);
                    }
                }
            }

            foreach (var circleRoad in createdCircleRoads)
            {
                foreach (var spawnPoint in circleRoad.spawnPoint)
                {
                    var selectedSpawnPoint = spawnPoint;

                    var spawn = prizes[0]; // You can adjust this to select a random prize if needed

                    var created = UnityEngine.Object.Instantiate(spawn, selectedSpawnPoint, true);
                    AdjustPositionToSpawnPoint(created, selectedSpawnPoint);

                    createdPrizes.Add(created);
                }
            }
            
        }

        private void AdjustPositionToSpawnPoint(GameObject created, Transform selectedSpawnPoint)
        {
            var position = created.transform.position;
            var spawnPosition = selectedSpawnPoint.position;

            position.x = spawnPosition.x;
            position.z = spawnPosition.z;

            created.transform.position = position;
        }
    }
}