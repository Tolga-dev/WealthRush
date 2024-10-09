using System;
using System.Collections.Generic;
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
        

        public void Start()
        {
            _gameManager = GameManager.Instance;

            _selections = new List<Selection>()
            {
                new()
                {
                    Action = Sum,
                    selectionAction = SelectionAction.Sum,
                    selectionOperation = "+"
                },
                new()
                {
                    Action = Subtraction,
                    selectionAction = SelectionAction.Subtraction,
                    selectionOperation = "-"
                },
                new()
                {
                    Action = Multiply,
                    selectionAction = SelectionAction.Multiply,
                    selectionOperation = "x"
                },
                new()
                {
                    Action = Divide,
                    selectionAction = SelectionAction.Divide,
                    selectionOperation = "/"
                }
            };
        }


        public void Sum(int value = 1)
        {
            Debug.Log($"Sum: {value}");

            var pileController = _gameManager.playerController.pileController;
            var prize = _gameManager.spawnerManager.prizeSpawner.prizes[0];
            
            for (int i = 0; i < value; i++)
            {
                pileController.AddPrizeToPile(Object.Instantiate(prize));
            }
        }

        public void Subtraction(int value = 1)
        {
            Debug.Log($"Subtraction: {value}");

            var pileController = _gameManager.playerController.pileController;
            var moneyPiles = pileController.moneyPiles;

            if (moneyPiles.Count == 0)
            {
                Debug.Log("you dead");
                return;
            }

            for (int i = 0; i < value && moneyPiles.Count > 0; i++)
            {
                var prize = moneyPiles[^1];
                moneyPiles.Remove(prize);
                Object.Destroy(prize);
            }
        }

        public void Multiply(int value = 1)
        {
            Debug.Log($"Multiply: {value}");

            var pileController = _gameManager.playerController.pileController;
            var moneyPiles = pileController.moneyPiles;

            if (moneyPiles.Count == 0) return;

            var prize = _gameManager.spawnerManager.prizeSpawner.prizes[0];

            for (int i = 0; i < value * moneyPiles.Count; i++)
            {
                pileController.AddPrizeToPile(Object.Instantiate(prize));
            }
        }

        public void Divide(int value = 1)
        {
            Debug.Log($"Divide: {value}");

            var pileController = _gameManager.playerController.pileController;
            var moneyPiles = pileController.moneyPiles;

            if (moneyPiles.Count == 0)
            {
                Debug.Log("No prizes to divide.");
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
        public int value;

        public void SetAction(Action<int> operation)
        {
            Action = operation;
        }

        public void PerformAction()
        {
            Action.Invoke(value); // Trigger the assigned action
        }
    }
}