using System;
using System.Collections.Generic;
using Managers.Controllers.Spawner;
using Save.GameObjects.Road;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public class SpawnerManager : MonoBehaviour
    {
        private GameManager _gameManager;
        
        [Header("Road Spawner")]
        public RoadSpawner roadSpawner;
        
        [Header("Obstacle Spawner")]
        public ObstacleSpawner obstacleSpawner;
        /*
        public GameObject prize;
        public float zOffsetAmount;
        public float maxXOffsetAmount;
        public float minXOffsetAmount;
        */
        
        private void Start()
        {
            _gameManager = GameManager.Instance;
            roadSpawner.Init();

            SpawnRoads();
            SpawnObstacles();
            /*PrizeSpawner();
            BossSpawner();*/
        }
        
        private void SpawnObstacles()
        {
            obstacleSpawner.SpawnObject(this);
        }

        private void SpawnRoads()
        {
            for (int i = 0; i < roadSpawner.GetNumberOfRoad(_gameManager); i++)
            {
                roadSpawner.SpawnNormalRoad();
            }
            
            if (_gameManager.currenLevel % 2 == 0) 
            {
                roadSpawner.SpawnCircleRoad();
            }

            roadSpawner.SpawnBossObject();

        }

        /*
        private void PrizeSpawner()
        {
            for (int i = 0; i < createdRoads.Count-1; i++)
            {
                var spawnPoints = createdRoads[i].spawnPoint;
                for (int j = 0; j < spawnPoints.Count; j++)
                {
                    var spawnPoint = spawnPoints[j];
                    if (j % 2 == 0)
                        continue;
    
                    Vector3 basePosition = spawnPoint.position;

                    int randomCubeCount = Random.Range(1, 10); // 1 to 9
                    float zOffset = 0;

                    for (int k = 0; k < randomCubeCount; k++)
                    {
                        float randomXOffset = Random.Range(minXOffsetAmount,maxXOffsetAmount); // Adjust the range as needed for randomness
                
                        var targetPos = basePosition + new Vector3(randomXOffset, 0, zOffset);
                        Instantiate(prize, targetPos, Quaternion.identity);

                        if (k % 2 == 0)
                        {
                            zOffset += (k + 1) * zOffsetAmount; // Increase zOffset by 5, 10, 20, etc.
                        }
                        else
                        {
                            zOffset -= (k + 1) * zOffsetAmount; // Decrease zOffset by 5, 10, 20, etc.
                        }
                    }
                }
            }
        }
        */

        /*private void ObstacleSpawner()
        {
            for (int i = 0; i < createdRoads.Count-1; i++)
            {
                var spawnPoints = createdRoads[i].spawnPoint;
                for (int j = 0; j < spawnPoints.Count; j++)
                {
                    var spawnPoint = spawnPoints[j];
                    obstacleSpawner.SetTransform(spawnPoint);
                    if(j % 2 == 0)
                    {
                      //  obstacleSpawner.SpawnObject(obstacleSpawner.obstacles[1].gameObject);
                    }
                    else
                    {
                        obstacleSpawner.SpawnObject(obstacleSpawner.obstacles[0].gameObject);
                    }
                }
            }
        }*/


        public void ResetSpawners()
        {
            roadSpawner.ResetRoads();

            SpawnRoads();
        }

    }
   
}