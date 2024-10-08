using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PileController
    {
        public Transform firstPilePosition;

        public List<GameObject> moneyPiles = new List<GameObject>();

        public void AddPrizeToPile(GameObject prize)
        {
            prize.transform.SetParent(firstPilePosition);

            var targetPosition = CalculateTargetPosition(prize.transform);
            CollectMoney(prize, targetPosition);
        }

        private Vector3 CalculateTargetPosition(Transform prizeTransform)
        {
            float heightAdjustment = prizeTransform.GetComponent<Renderer>().bounds.size.y;

            Vector3 newPosition = firstPilePosition.position;
            newPosition.y += (moneyPiles.Count * heightAdjustment) + heightAdjustment / 2; // Adjust position
            
            return newPosition;
        }
        
        private void CollectMoney(GameObject prize, Vector3 finalPosition)
        {
            prize.transform.position = finalPosition;
            moneyPiles.Add(prize);
        }
    }
}