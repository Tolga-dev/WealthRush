using Managers;
using TMPro;
using UnityEngine;

namespace Save.GameObjects.Prizes
{
    public class Selector : Prize
    {
        public Selection selection;
        public TextMeshPro selectionText;

        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (isHitPlayer)
            {
                selection.PerformAction();
            }
        }
        
        public void SetText()
        {
            selectionText.text = selection.selectionOperation +" " + selection.value;
            
           
        }
        
    }
}