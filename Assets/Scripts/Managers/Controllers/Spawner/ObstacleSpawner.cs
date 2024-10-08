using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers.Controllers.Spawner
{
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