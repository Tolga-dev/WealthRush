using System.Collections;
using Player;
using UnityEngine;

namespace Save.GameObjects.Road
{
    public class BossRoad : MonoBehaviour
    {
        public GameObject boss;
        public float scaleIncreasePerPile = 0.1f; // How much the scale increases per money pile
        public float scaleDuration = 0.5f; // Duration of the scaling animation for each pile
        public float moveDuration = 1f; // Duration for moving the money pile to the boss

        public void PlayerArrived(PlayerController playerController)
        {
            var moneyPiles = playerController.pileController.moneyPiles;
            foreach (var moneyPile in moneyPiles)
            {
                StartCoroutine(MovePileToBossAndScale(moneyPile));
            }
        }

        private IEnumerator MovePileToBossAndScale(GameObject moneyPile)
        {
            // Move money pile to the boss position
            Vector3 initialPosition = moneyPile.transform.position;
            Vector3 bossPosition = boss.transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                moneyPile.transform.position = Vector3.Lerp(initialPosition, bossPosition, (elapsedTime / moveDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the money pile is exactly at the boss position
            moneyPile.transform.position = bossPosition;

            // Optionally disable the money pile after it reaches the boss
            moneyPile.SetActive(false);

            // Scale the boss after each money pile arrives
            StartCoroutine(ScaleBoss());
        }

        private IEnumerator ScaleBoss()
        {
            Vector3 initialScale = boss.transform.localScale;
            Vector3 targetScale = initialScale + new Vector3(scaleIncreasePerPile, scaleIncreasePerPile, scaleIncreasePerPile);
            float elapsedTime = 0f;

            while (elapsedTime < scaleDuration)
            {
                boss.transform.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / scaleDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the final scale is exactly the target scale
            boss.transform.localScale = targetScale;
        }

    }
}