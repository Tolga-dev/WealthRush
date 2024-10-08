using System;
using System.Collections.Generic;
using Save.GameObjects.Road;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers.Controllers.Spawner
{
    [Serializable]
    public class PrizeSpawner
    {
        public List<GameObject> prizes = new List<GameObject>();
        public GameObject chest;
        
        public List<GameObject> createdPrizes = new List<GameObject>();
        
        private int chestSpawnCount = 0; 
        private const int maxChestSpawns = 3;
        
        public void SpawnObject(SpawnerManager spawnerManager)
        {
            SpawnPrizes(spawnerManager.roadSpawner.createdRoads);
            SpawnPrizes(spawnerManager.roadSpawner.createdCircleRoads);
            
            if (chestSpawnCount < maxChestSpawns)
            {
                SpawnChest(spawnerManager);
                // chestSpawnCount++; // when game is finished
            }
            
        }

        private void SpawnPrizes(IEnumerable<Road> roads)
        {
            foreach (var road in roads)
            {
                for (int i = 0; i < road.spawnPoint.Count; i++)
                {
                    if (!road.isObjSpawned[i])
                    {
                        CreatePrize(road.spawnPoint[i]);
                    }
                }
            }
        }

        private void CreatePrize(Transform spawnPoint)
        {
            var rate = Random.Range(0, 200);
            var index = rate > 100 ? 0 : rate > 12.5 ? 1 : 2;
            
            var prizePrefab = prizes[index];  
            var prize = UnityEngine.Object.Instantiate(prizePrefab, spawnPoint, true);
            var position = spawnPoint.position;
            
            prize.transform.position = new Vector3(position.x, prize.transform.position.y, position.z);
            createdPrizes.Add(prize);
        }
        private void SpawnChest(SpawnerManager spawnerManager)
        {
            var road = spawnerManager.roadSpawner.createdRoads[0]; // make it easy some times :)
            var spawnPoint = road.spawnPoint[Random.Range(0, road.spawnPoint.Count)];
            SpawnAtPoint(chest, spawnPoint);
        }
        private void SpawnAtPoint(GameObject prize, Transform spawnPoint)
        {
            var created = UnityEngine.Object.Instantiate(prize, spawnPoint.position, Quaternion.identity);
            createdPrizes.Add(created);
        }
    }
}