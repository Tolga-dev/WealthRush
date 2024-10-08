using Save.GameObjects.Base;
using UnityEngine;

namespace Save.GameObjects.Prizes
{
    public class Prize : GameObjectBase
    {
        public ParticleSystem emoji;
        public int prizeAmount;

        protected override void PlayAdditionalEffects()
        {
            if (emoji != null)
            {
                emoji.Play();
            }
        }
    }
}