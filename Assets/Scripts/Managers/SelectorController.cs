using System;
using System.Collections.Generic;
using Save.GameObjects.Prizes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public enum SelectionAction
    {
        Sum,
        Subtraction,
        Multiply,
        Divide
    }

    [Serializable]
    public class SelectorController
    {
        private List<Selection> _selections;
        private GameManager _gameManager;

        public Sprite positiveSprite;
        public Sprite badSprite;

        public void Start()
        {
            _gameManager = GameManager.Instance;

            _selections = new List<Selection>()
            {
                new()
                {
                    Action = Sum,
                    selectionAction = SelectionAction.Sum,
                    selectionOperation = "+",
                    sprite = positiveSprite
                },
                new()
                {
                    Action = Subtraction,
                    selectionAction = SelectionAction.Subtraction,
                    selectionOperation = "-",
                    sprite = badSprite
                }/*,
                new()
                {
                    Action = Multiply,
                    selectionAction = SelectionAction.Multiply,
                    selectionOperation = "x",
                    sprite = positiveSprite
                },
                new()
                {
                    Action = Divide,
                    selectionAction = SelectionAction.Divide,
                    selectionOperation = "/",
                    sprite = badSprite
                }*/
            };
        }


        public void Sum(int value = 1)
        {
            var pileController = _gameManager.playerController.pileController;
            var prize = _gameManager.spawnerManager.prizeSpawner.standardPrize;
            
            for (int i = 0; i < value; i++)
            {
                pileController.AddPrizeToPile(_gameManager.objectPoolManager.GetPile(), prize.prizeAmount);
            }
            
        }

        public void Subtraction(int value = 1)
        {
            var pileController = _gameManager.playerController.pileController;
            var moneyPiles = pileController.moneyPiles;

            if (moneyPiles.Count == 0)
            {
                return;
            }

            for (int i = 0; i < value && moneyPiles.Count > 0; i++)
            {
                var prize = moneyPiles[^1];
                _gameManager.playingState.score -= prize.prizeAmount;
                _gameManager.playingState.UpdateScore();

                if (prize.isPoolPrize)
                {
                    _gameManager.objectPoolManager.SetPile(prize);
                    moneyPiles.Remove(prize);
                }
                else
                {
                    Object.Destroy(prize.gameObject);
                    moneyPiles.Remove(prize);
                }
            }
        }

        public void Multiply(int value = 1)
        {
            var pileController = _gameManager.playerController.pileController;
            var moneyPiles = pileController.moneyPiles;

            if (moneyPiles.Count == 0) return;

            var prize = _gameManager.spawnerManager.prizeSpawner.standardPrize;
            var amount = value * moneyPiles.Count;
            
            for (int i = 0; i < amount; i++)
            {
                pileController.AddPrizeToPile(Object.Instantiate(prize).GetComponent<Prize>());
            }
        }

        public void Divide(int value = 1)
        {
            var pileController = _gameManager.playerController.pileController;
            var moneyPiles = pileController.moneyPiles;

            if (moneyPiles.Count == 0)
            {
                return;
            }

            int divideCount = Mathf.Min(moneyPiles.Count, value);

            for (int i = 0; i < divideCount; i++)
            {
                var prize = moneyPiles[^1];
                moneyPiles.Remove(prize);
                Object.Destroy(prize);
            }
        }

        public List<Selection> GetOperations() => _selections;


    }

    [Serializable]
    public class Selection
    {
        public SelectionAction selectionAction;
        public Action<int> Action;
        public string selectionOperation;
        public Sprite sprite;
        public void SetAction(Action<int> operation)
        {
            Action = operation;
        }

        public void PerformAction(int value)
        {
            Action.Invoke(value); // Trigger the assigned action
        }
    }
}