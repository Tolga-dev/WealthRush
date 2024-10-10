using System;
using System.Collections.Generic;
using Managers;
using Save.GameObjects.Prizes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Player
{
    [Serializable]
    public class PileController
    {
        public Transform firstPilePosition;
        public float initHigh;

        public List<GameObject> moneyPiles = new List<GameObject>();
        public Chest foundChest;
        public float heightAdjustment = 0.1f;

        private PlayerController _playerController;
        
        public void Init(PlayerController playerController)
        {
            initHigh = firstPilePosition.position.y;
            _playerController = playerController;
        }
        public void AddPrizeToPile(GameObject prize)
        {
            prize.transform.SetParent(firstPilePosition);

            var targetPosition = CalculateTargetPosition(prize.transform);
            CollectMoney(prize, targetPosition);

            var prizeAmount = prize.GetComponent<Prize>().prizeAmount;

            _playerController.gameManager.playingState.score += prizeAmount;
            _playerController.gameManager.playingState.UpdateScore();
        }

        private Vector3 CalculateTargetPosition(Transform prizeTransform)
        {
            Vector3 newPosition = firstPilePosition.position;
            newPosition.y += moneyPiles.Count * heightAdjustment; // Adjust position
            
            return newPosition;
        }
        
        private void CollectMoney(GameObject prize, Vector3 finalPosition)
        {
            prize.transform.position = finalPosition;
            moneyPiles.Add(prize);
        }

        public void ResetPile()
        {
            var vector3 = firstPilePosition.position;
            vector3.y = initHigh;
            firstPilePosition.position = vector3;
            
            foreach (var moneyPile in moneyPiles)
            {
                Object.Destroy(moneyPile);
            }
            moneyPiles.Clear();

            if (foundChest != null)
            {
                Object.Destroy(foundChest);
                foundChest = null;
            }
        }
    }
}