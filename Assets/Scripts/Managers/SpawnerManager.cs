using System;
using System.Collections.Generic;
using Managers.Controllers.Spawner;
using Save.GameObjects.Obstacle;
using Save.GameObjects.Road;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnerManager : MonoBehaviour
    {
        public GameManager gameManager;

        public RoadSpawner roadSpawner;
        public ObstacleSpawner obstacleSpawner;
        public GameObject prize;
        public float zOffsetAmount;
        public float maxXOffsetAmount;
        public float minXOffsetAmount;
        
        public List<Road> createdRoads = new List<Road>();

        private void Start()
        {
            RoadSpawner();
            ObstacleSpawner();
            PrizeSpawner();
            BossSpawner();
        }

        private void BossSpawner()
        {
        }

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

        private void ObstacleSpawner()
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
        }

        private void RoadSpawner()
        {
            for (int i = 0; i < 3; i++)
            {
                var created = roadSpawner.SpawnObject();
                createdRoads.Add(created.GetComponent<Road>());
            }

            var boss = roadSpawner.SpawnBossObject();
            createdRoads.Add(boss.GetComponent<Road>());
        }
    }
    [Serializable]
    public class ObstacleSpawner
    {
        private Transform _spawnPoint;
        public List<GameObject> obstacles = new List<GameObject>();

        public virtual void SpawnObject(GameObject spawn = null)
        {
            var created = Object.Instantiate(spawn, _spawnPoint, true);
            
            var position = created.transform.position;
            position.x = _spawnPoint.position.x;
            position.z = _spawnPoint.position.z;

            created.transform.position = position;
        }
        
        public void SetTransform(Transform newTransform)
        {
            _spawnPoint = newTransform;
        }
    }    
}