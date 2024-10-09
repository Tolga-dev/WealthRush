using System;
using System.Collections.Generic;
using Save.GameObjects.Prizes;
using Save.GameObjects.Road;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Managers.Controllers.Spawner
{
    [Serializable]
    public class PrizeSpawner
    {
        public List<GameObject> prizes = new List<GameObject>();
        public GameObject chest;
        
        public List<GameObject> createdPrizes = new List<GameObject>();
        
        private SpawnerManager _spawnerManager;
        public void SpawnObject(SpawnerManager spawnerManager)
        {
            _spawnerManager = spawnerManager;
            
            SpawnPrizes(spawnerManager.roadSpawner.createdRoads);
            SpawnPrizes(spawnerManager.roadSpawner.createdCircleRoads);

            var save = spawnerManager.GameManager.gamePropertiesInSave;
            if (save.chestSpawnCount < save.maxChestSpawns)
            {
                SpawnChest(spawnerManager);
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
            
            int index = rate switch
            {
                > 120 => 0,
                > 100 => 1,
                > 45 => 2,
                _ => 3
            };

            var prizePrefab = prizes[index];

            int spawnCount = index switch
            {
                0 => 10,
                1 => 4,
                2 => 1,
                _ => 2
            };

            float[] positionOffsets = { 0, 1, -1, 2, -2, 3, -3, 4, -4, 5 }; // Covers all potential spawn counts for index 0
            
            
            for (int i = 0; i < spawnCount; i++)
            {
                var prize = UnityEngine.Object.Instantiate(prizePrefab, spawnPoint, true);
                var position = spawnPoint.position;

                float randomXOffset = Random.Range(-2f, 2f);
        
                prize.transform.position = new Vector3(position.x + randomXOffset, prize.transform.position.y, position.z + positionOffsets[i]);
                createdPrizes.Add(prize);

                if (index == 2)
                {
                    ConfigureSelector(prize);
                }
            }
        }

        private void ConfigureSelector(GameObject prize)
        {
            var selector = prize.GetComponentInChildren<Selector>();
            var gameManager = _spawnerManager.GameManager;
            var selectorManager = gameManager.selectorManager;
            var operations = selectorManager.GetOperations();
            
            selector.selection = operations[Random.Range(0, operations.Count)];
    
            var currentLevel = gameManager.gamePropertiesInSave.currenLevel;

            selector.selection.value = selector.selection.selectionAction switch
            {
                SelectionAction.Sum => Random.Range(5, 20 + currentLevel),
                SelectionAction.Subtraction => Random.Range(5, 20 + currentLevel),
                SelectionAction.Multiply => Random.Range(2, 10 + currentLevel),
                SelectionAction.Divide => Random.Range(2, 10 + currentLevel),
                _ => selector.selection.value
            };
            
            selector.SetText();
            
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

        public void ResetPrize()
        {
            foreach (var createdObstacle in createdPrizes)
            {
                Object.Destroy(createdObstacle);
            }
            createdPrizes.Clear();
        }
    }
}