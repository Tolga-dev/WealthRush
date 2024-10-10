using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Save.GameObjects.Prizes;
using Save.GameSo;
using UnityEngine;

namespace Save.GameObjects.Road
{
    public class BossRoad : Road
    {
        private PlayerController _playerController;
        private GamePropertiesInSave _save;
        
        [Header("Boss")] 
        public GameObject boss;
        public float scaleDuration = 0.5f; // Duration of the scaling animation for each pile
        public float moveDuration = 0.5f; // Duration for moving the money pile to the boss
        private readonly Vector3 _maxScale = new Vector3(27.023634f, 810.709106f, 27.023634f);

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerController = other.GetComponent<PlayerController>();
                _save = _playerController.gameManager.gamePropertiesInSave;
                PlayerArrived();
            }
        }

        private void PlayerArrived() // player finished game, add score in here
        {
            SetActiveReloadButton(false);
            
            _playerController.SetWin();
            
            CheckForChest();
            AddComboBonus();

            _playerController.gameManager.PlayASound(_playerController.gameManager.onGameWinSound);

            var moneyPiles = _playerController.pileController.moneyPiles;
            if (moneyPiles.Count == 0)
            {
                _playerController.gameManager.StartCoroutine(SetGameMainMenu());
            }
            else
            {
                StartCoroutine(FinishedGameWithPile(moneyPiles));
            }
        }

        private IEnumerator FinishedGameWithPile(List<Prize> moneyPiles)
        {
            foreach (var moneyPile in moneyPiles)
            {
                StartCoroutine(MovePileToBossAndScale(moneyPile));
            }

            yield return StartCoroutine(SetGameMainMenu());
        }


        private IEnumerator MovePileToBossAndScale(Prize moneyPile)
        {
            var initialPosition = moneyPile.transform.position;
            var bossPosition = boss.transform.position;
            var elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                moneyPile.transform.position = Vector3.Lerp(initialPosition, bossPosition, (elapsedTime / moveDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            moneyPile.transform.position = bossPosition;
            moneyPile.gameObject.SetActive(false);

            CheckForRecords();
            StartCoroutine(ScaleBoss());
        }

        private IEnumerator ScaleBoss()
        {
            Vector3 initialScale = boss.transform.localScale;
            Vector3 targetScale = initialScale + initialScale * (_playerController.pileController.moneyPiles.Count * 0.1f);

            // Ensure target scale does not exceed the maximum allowed scale
            targetScale = Vector3.Min(targetScale, _maxScale);

            float elapsedTime = 0f;

            while (elapsedTime < scaleDuration)
            {
                // Lerp the scale and clamp it to not exceed the maximum scale
                boss.transform.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / scaleDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set the final scale to the clamped target scale
            boss.transform.localScale = targetScale;
        }

        private IEnumerator SetGameMainMenu()
        {
            Debug.Log("Called");
            yield return new WaitForSeconds(3);
            var gameManager = _playerController.gameManager;
            gameManager.ChangeState(gameManager.menuState);
            SetActiveReloadButton(true);
        }

        private void CheckForRecords()
        {
            var score = _playerController.gameManager.playingState.score;

            var findRecord = -1;
            for (int i = 0; i < _save.levelRecords.Length; i++)
            {
                if (score > _save.levelRecords[i])
                {
                    findRecord = i;
                }
            }

            if (findRecord != -1)
            {
                MadeAScore(_save.levelRecords[findRecord]);
            }
        }
        private void MadeAScore(int recordIndexBonus)
        {
            StartCoroutine(IncreaseBonusOverTime(recordIndexBonus, 1f, "record"));
        }

        private IEnumerator IncreaseBonusOverTime(int targetValue, float duration, string bonusType)
        {
            var playingState = _playerController.gameManager.playingState;
    
            float elapsed = 0f;
            int startValue = 0; // Start from 0 and increase over time
            int currentScore = bonusType == "record" ? 0 : playingState.score; // Adjust based on bonus type

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
        
                int currentValue = (int)(Mathf.Lerp(startValue, targetValue, elapsed / duration));

                if (bonusType == "record")
                {
                    playingState.extraBonus.text = $"WAOW Record! Bonus: {currentValue}";
                }
                else if (bonusType == "combo")
                {
                    playingState.score = currentScore + currentValue;
                    playingState.extraComboBonus.text = $"Combo Bonus: {currentValue}";
                }

                yield return null;
            }

            // Ensure final value is set to target when finished
            if (bonusType == "record")
            {
                playingState.extraBonus.text = $"WAOW Record! Bonus: {targetValue}";
            }
            else if (bonusType == "combo")
            {
                playingState.score = currentScore + targetValue;
                playingState.extraComboBonus.text = $"Combo Bonus: {targetValue}";
            }
        }
        private void SetActiveReloadButton(bool b)
        {
            _playerController.gameManager.playingState.reloadButton.enabled = b;
        }

        private void CheckForChest()
        {
            var foundChest = _playerController.pileController.foundChest;
            if (foundChest != null)
            {
                _save.chestSpawnCount++;
            }
        }
        private void AddComboBonus()
        {
            var comboRank = _playerController.gameManager.playingState.score * _save.comboRank / 100;
            StartCoroutine(IncreaseBonusOverTime(comboRank, 1f, "combo"));
        }
        
    }
}