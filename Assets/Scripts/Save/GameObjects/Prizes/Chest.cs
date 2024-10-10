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
                var playerController = other.GetComponentInChildren<PlayerController>();
                if (playerController == null) // gives null idk
                    return;
                playerController.pileController.foundChest = this;
            }
        }
    }
}