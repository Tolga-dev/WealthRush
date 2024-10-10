using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Save.GameObjects.Prizes
{
    public class Chest : Prize
    {
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (isHitPlayer)
            {
                var playerController = other.GetComponent<PlayerController>();
                playerController.pileController.foundChest = this;
            }
        }
        public override IEnumerator AnimateCanvas(Transform canvasTransform)
        {
            yield return base.AnimateCanvas(canvasTransform);
            StartCoroutine(CloseActive());
        }
        
        private IEnumerator CloseActive()
        {
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }
    }
}