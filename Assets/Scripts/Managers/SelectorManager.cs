using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public enum SelectionAction
    {
        Sum,
        Subtraction,
        Multiply,
        Divide
    }
    
    public class SelectorManager : MonoBehaviour
    {
        private List<Selection> _selections;
        public GameManager gameManager;

        private void Start()
        {
            _selections = new List<Selection>()
            {
                new()
                {
                    Action = Sum,
                    selectionAction = SelectionAction.Sum
                },
                new()
                {
                    Action = Subtraction,
                    selectionAction = SelectionAction.Subtraction
                },
                new()
                {
                    Action = Multiply,
                    selectionAction = SelectionAction.Multiply
                },
                new()
                {
                    Action = Divide,
                    selectionAction = SelectionAction.Divide
                }
            };
        }

        public void PerformSelection(SelectionAction selectedSelectionAction)
        {
            foreach (var selection in _selections)
            {
                if(selection.selectionAction == selectedSelectionAction)
                    selection.PerformAction();
            }
            
        }
        
        public void Sum()
        {
            var pileController = gameManager.playerController.pileController;
            var moneyPiles = pileController.moneyPiles;

            pileController.AddPrizeToPile(Instantiate(moneyPiles[0]));
        }
        public void Subtraction()
        {
                
            var pileController = gameManager.playerController.pileController;
            var moneyPiles = pileController.moneyPiles;
            if (moneyPiles.Count == 0)
            {
                Debug.Log("you dead");
                return;
            }

            var prize = moneyPiles[^1];
            
            moneyPiles.Remove(prize);
            Destroy(prize);
        }

        public void Multiply()
        {
            
        }

        public void Divide()
        {
            
            
        }


    }
    
    [Serializable]
    public class Selection
    {
        public SelectionAction selectionAction;
        public Action Action;

        public void SetAction(Action operation)
        {
            Action = operation;
        }
        
        public void PerformAction()
        {
            Action.Invoke(); // Trigger the assigned action
        }
    }
}