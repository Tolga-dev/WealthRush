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
                gameObject.SetActive(false);
            }
        }
    }
}