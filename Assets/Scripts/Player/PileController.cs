using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PileController
    {
        public Transform firstPilePosition;

        public List<GameObject> moneyPiles = new List<GameObject>();
        public float heightAdjustment = 0.1f;

        private PlayerController _playerController;
        
        public void Init(PlayerController playerController)
        {
            _playerController = playerController;
        }
        public void AddPrizeToPile(GameObject prize)
        {
            prize.transform.SetParent(firstPilePosition);

            var targetPosition = CalculateTargetPosition(prize.transform);
            CollectMoney(prize, targetPosition);
            
            _playerController.gameManager.playingState.score++;
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
    }
}