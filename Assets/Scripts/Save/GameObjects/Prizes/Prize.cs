using Save.GameObjects.Base;
using UnityEngine;

namespace Save.GameObjects.Prizes
{
    public class Prize : GameObjectBase
    {
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            
            if (isHitPlayer == false)
            {
                
            }
        }
        
    }
}