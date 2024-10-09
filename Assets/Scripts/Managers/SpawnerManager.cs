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
        
        [Header("Prize Spawner")]
        public PrizeSpawner prizeSpawner;
        
        private void Start()
        {
            _gameManager = GameManager.Instance;
            roadSpawner.Init();

            SpawnRoads();
            SpawnObstacles();
            SpawnerPrizes();
        }

        private void SpawnerPrizes()
        {
            prizeSpawner.SpawnObject(this);
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

        public void ResetSpawners()
        {
            roadSpawner.ResetRoads();

            SpawnRoads();
        }
        public GameManager GameManager
        {
            get => _gameManager;
            set => _gameManager = value;
        }
    }
   
}