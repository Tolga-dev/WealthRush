using System;
using System.Collections;
using Player;
using Save.GameObjects.Prizes;
using UnityEngine;

namespace Save.GameObjects.Road
{
    public class BossRoad : Road
    {
        private PlayerController _playerController;
        
        public GameObject boss;
        public float scaleDuration = 0.5f; // Duration of the scaling animation for each pile
        public float moveDuration = 0.5f; // Duration for moving the money pile to the boss
        private readonly Vector3 _maxScale = new Vector3(27.023634f, 810.709106f, 27.023634f);

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerController = other.GetComponent<PlayerController>();
                PlayerArrived();
            }
        }
        
        public void PlayerArrived()
        {
            SetActiveReloadButton(false);
            _playerController.SetWin();
            _playerController.gameManager.SwitchToWinCam();
            CheckForChest();
            AddComboBonus();

            _playerController.gameManager.PlayASound(_playerController.gameManager.onGameWinSound);
            _playerController.gameManager.gamePropertiesInSave.currenLevel++;
            _playerController.gameManager.playingState.isGameWon = true;

            var moneyPiles = _playerController.pileController.moneyPiles;
            if (moneyPiles.Count == 0)
            {
                _playerController.gameManager.StartCoroutine(SetGameMainMenu());
                return;
            }
            
            foreach (var moneyPile in moneyPiles)
            {
                _playerController.gameManager.StartCoroutine(MovePileToBossAndScale(moneyPile));
            }
        }

        private void SetActiveReloadButton(bool b)
        {
            _playerController.gameManager.playingState.reloadButton.enabled = b;
        }

        private IEnumerator MovePileToBossAndScale(Prize moneyPile)
        {
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
            StartCoroutine(SetGameMainMenu());
        }

        private IEnumerator SetGameMainMenu()
        {
            yield return new WaitForSeconds(3);
            var gameManager = _playerController.gameManager;
            gameManager.ChangeState(gameManager.menuState);
            SetActiveReloadButton(true);
        }
        private void CheckForChest()
        {
            var foundChest = _playerController.pileController.foundChest;
            if (foundChest != null)
            {
                var save = _playerController.gameManager.gamePropertiesInSave;
                save.chestSpawnCount++;
                
            }
        }
        private void CheckForRecords()
        {
            var score = _playerController.gameManager.playingState.score;
            var save = _playerController.gameManager.gamePropertiesInSave;

            var findRecord = -1;
            for (int i = 0; i < save.levelRecords.Length; i++)
            {
                if (score > save.levelRecords[i])
                {
                    findRecord = i;
                }
            }

            if (findRecord != -1)
            {
                MadeAScore(save.levelRecords[findRecord]);
            }
        }
        private void MadeAScore(int recordIndexBonus)
        {
            StartCoroutine(IncreaseBonusOverTime(recordIndexBonus, 1f, "record"));
        }

        private void AddComboBonus()
        {
            var save = _playerController.gameManager.gamePropertiesInSave;
            var comboRank = _playerController.gameManager.playingState.score * save.comboRank / 100;
            StartCoroutine(IncreaseBonusOverTime(comboRank, 1f, "combo"));
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

        

    }
}