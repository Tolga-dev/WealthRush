using System;
using Managers;
using Save.GameObjects.Base;
using UnityEngine;

namespace Save.GameObjects.Obstacle
{
    public class Obstacle : GameObjectBase
    {
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            
            if (isHitPlayer == false)
            {
                if (other.CompareTag("Money"))
                {
                    CallMoneyPileGotHit(other.gameObject);
                }
            }
        }

        protected virtual void CallMoneyPileGotHit(GameObject money)
        {
            Debug.Log("Money Pile Got Hit");
            
            /*var moneyPiles = gameManager.playerController.pileController.moneyPiles;

            var foundIndex = 0;
            for (int i = 0; i < moneyPiles.Count; i++)
            {
                if (moneyPiles[i] == money)
                {
                    foundIndex = i;
                    break; // Stop the loop once we find the index
                } 
            }

            var removeCount = moneyPiles.Count - foundIndex;

            for (int i = 0; i < removeCount; i++)
            {
                var pile = moneyPiles[^1]; // Get the last pile
                pile.SetActive(false);
                
                moneyPiles.RemoveAt(moneyPiles.Count - 1);   // Remove it from the list
                Destroy(pile);                               // Destroy the GameObject
            }*/
        }



        
    }
}