using System.Collections.Generic;
using Save.GameObjects.Prizes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public GameManager gameManager;
        public Transform spawnPoint;
        public int size = 200;

        public List<Prize> moneyPiles = new List<Prize>();

        private void Start()
        {
            for (int i = 0; i < size; i++)
            {
                var money = Instantiate(gameManager.spawnerManager.prizeSpawner.standardPrize, spawnPoint);
                money.isPoolPrize = true;
                money.gameObject.SetActive(false);
                moneyPiles.Add(money); // Add to list
            }
        }

        public Prize GetPile()
        {
            foreach (var pile in moneyPiles)
            {
                if (!pile.gameObject.activeInHierarchy)
                {
                    pile.gameObject.SetActive(true);
                    return pile;
                }
            }
            // If no inactive pile is found, optionally expand the pool here
            return null;
        }

        public void SetPile(Prize setPile)
        {
            setPile.transform.parent = spawnPoint;
            setPile.gameObject.SetActive(false);
            setPile.onUse = false;
        }
    }


}