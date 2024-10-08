using System;
using Managers;
using UnityEngine;

namespace Save.GameObjects.Obstacle
{
    public enum Obstacles
    {
        Knife,
        Brick
    }
    public class Obstacle : MonoBehaviour
    {
        public Obstacles obstacles;
        public GameManager gameManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CallPlayerGotHit();
            }
            else if (other.CompareTag("Money"))
            {
                CallMoneyPileGotHit(other.gameObject);
            }
        }

        protected virtual void CallMoneyPileGotHit(GameObject money)
        {
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

            for (int i = 0; i < removeCount; i++)
            {
                var pile = moneyPiles[^1]; // Get the last pile
                pile.SetActive(false);
                
                moneyPiles.RemoveAt(moneyPiles.Count - 1);   // Remove it from the list
                Destroy(pile);                               // Destroy the GameObject
            }
        }


        protected virtual void  CallPlayerGotHit()
        {
            Debug.Log("Game Over");
        }
        
    }
}