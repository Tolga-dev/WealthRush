using System;
using System.Collections;
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
        
        public void Starter()
        {
            _gameManager = GameManager.Instance;
            roadSpawner.Init();

            SpawnObjects(); 
        }

        private void SpawnObjects()
        {
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
            
            if (_gameManager.gamePropertiesInSave.currenLevel % 2 == 0) 
            {
                roadSpawner.SpawnCircleRoad();
            }

            roadSpawner.SpawnBossObject();

        }

        public IEnumerator ResetSpawners()
        {
            
            roadSpawner.ResetRoads();
            obstacleSpawner.ResetObstacle();
            prizeSpawner.ResetPrize();

            SpawnObjects();

            yield return null;
        }
        public GameManager GameManager
        {
            get => _gameManager;
            set => _gameManager = value;
        }
    }
   
}