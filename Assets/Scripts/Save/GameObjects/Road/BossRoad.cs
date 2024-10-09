using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Save.GameObjects.Road
{
    public class BossRoad : Road
    {
        private PlayerController _playerController;
        
        public GameObject boss;
        public float scaleDuration = 0.5f; // Duration of the scaling animation for each pile
        public float moveDuration = 0.5f; // Duration for moving the money pile to the boss

        public void PlayerArrived()
        {
            _playerController.SetWin();
            _playerController.gameManager.PlayASound(_playerController.gameManager.onGameWinSound);
            _playerController.gameManager.gamePropertiesInSave.currenLevel++;
            _playerController.gameManager.playingState.isGameWon = true;

            var moneyPiles = _playerController.pileController.moneyPiles;
            if (moneyPiles.Count == 0)
            {
                StartCoroutine(SetGameMainMenu());
                return;
            }
            
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

            moneyPile.transform.position = bossPosition;
            moneyPile.SetActive(false);

            StartCoroutine(ScaleBoss());
        }

        private IEnumerator ScaleBoss()
        {
            Vector3 initialScale = boss.transform.localScale;
            Vector3 targetScale = initialScale + initialScale * (_playerController.pileController.moneyPiles.Count * 0.1f);
            float elapsedTime = 0f;

            while (elapsedTime < scaleDuration)
            {
                boss.transform.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / scaleDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            boss.transform.localScale = targetScale;

            StartCoroutine(SetGameMainMenu());
        }

        private IEnumerator SetGameMainMenu()
        {
            yield return new WaitForSeconds(2);
            var gameManager = _playerController.gameManager;
            gameManager.ChangeState(gameManager.menuState);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerController = other.GetComponent<PlayerController>();
                PlayerArrived();
            }
        }
    }
}