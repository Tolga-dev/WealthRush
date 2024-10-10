using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Save.GameObjects.Prizes
{
    public class Selector : Prize
    {
        public Selection selection;
        public TextMeshPro selectionText;
        public SpriteRenderer gateSprite;
        
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