using System;
using Managers;
using Save.GameObjects.Base;
using Save.GameObjects.Prizes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Save.GameObjects.Obstacle
{
    public class Obstacle : GameObjectBase
    {
        public GameManager gameManager;

        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            
            if (other.CompareTag("Money"))
            {
                var prize = other.GetComponentInChildren<Prize>();
                if (prize.onUse == false)
                    return;
                
                CallMoneyPileGotHit(prize);
            }
        }

        protected virtual void CallMoneyPileGotHit(Prize money)
        {
            Debug.Log("Money Pile Got Hit");
            
            var moneyPiles = gameManager.playerController.pileController.moneyPiles;

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
            
            gameManager.selectorManager.Subtraction(removeCount);

        }

        protected override void DisableGameObject()
        {
            
        }
        
    }
}