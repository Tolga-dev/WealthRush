using System;
using Save.GameObjects;
using System;
using System.Collections.Generic;
using Cinemachine;
using Save.GameObjects;
using Save.GameObjects.Road;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
namespace Managers.Controllers.Spawner
{
    [Serializable]
    public class RoadSpawner
    {
        public GameObject road;
        public GameObject circleRoad;
        public GameObject bossRoad;
    
        public Vector3 offset;
        public Transform spawnPoint;

        public Vector3 initPos;
        
        public List<Road> createdRoads = new List<Road>();
        public List<Road> createdCircleRoads = new List<Road>();
        public BossRoad createdBossRoad;
        
        public int startAmountOfRoad = 2;

        public CinemachineVirtualCamera prizeCamVirtualCamera;
        public void Init()
        {
            initPos = spawnPoint.position;
        }
        public void SpawnNormalRoad()
        {
            var created = Object.Instantiate(road, spawnPoint.position,road.transform.rotation);
            createdRoads.Add(created.GetComponent<Road>());
            SetNewPos();
        }
        public void SpawnCircleRoad()
        {
            var created = Object.Instantiate(circleRoad, spawnPoint.position,circleRoad.transform.rotation);
            createdCircleRoads.Add(created.GetComponent<Road>());
            SetNewPos();
        }
        
        public void SpawnBossObject()
        {
            var created = Object.Instantiate(bossRoad, spawnPoint.position, bossRoad.transform.rotation);
            createdBossRoad = created.GetComponent<BossRoad>();
            var transform = createdBossRoad.transform;
            prizeCamVirtualCamera.LookAt = transform;
            prizeCamVirtualCamera.Follow = transform;
            
            SetNewPos();
        }

        public Vector3 SetNewPos()
        {
            return spawnPoint.position += offset; 
        }
        
        public void ResetRoads()
        {
            foreach (var createdRoad in createdRoads)
            {
                Object.Destroy(createdRoad.gameObject);
            }
            createdRoads.Clear();
            
            foreach (var createdRoad in createdCircleRoads) 
            {
                Object.Destroy(createdRoad.gameObject);
            }
            createdCircleRoads.Clear();

            Object.Destroy(createdBossRoad.gameObject);
            createdBossRoad = null;
            
            spawnPoint.position = initPos;
        }


        public int GetNumberOfRoad(GameManager gameManager)
        {
            var level = gameManager.gamePropertiesInSave.currenLevel;
            var numberOfRoadsToSpawn = startAmountOfRoad;

            numberOfRoadsToSpawn += level / 10;
            
            return numberOfRoadsToSpawn;
        }
    }

}