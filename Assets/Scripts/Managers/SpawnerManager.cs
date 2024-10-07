using System;
using System.Collections.Generic;
using Managers.Controllers.Spawner;
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
    

}