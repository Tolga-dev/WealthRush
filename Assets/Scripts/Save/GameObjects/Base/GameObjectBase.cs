using System;
using Managers;
using UnityEngine;

namespace Save.GameObjects.Base
{
    public class GameObjectBase : MonoBehaviour
    {
        [Header("Components")]
        public Animator animator;

        public AudioClip playerHitSound;
        public ParticleSystem particleSystem;

        public bool isHitPlayer = false;
        private static readonly int PlayerHit = Animator.StringToHash("HitPlayer");

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CallPlayerGotHit();
            }
        }
        protected virtual void  CallPlayerGotHit()
        {
            GameManager.Instance.PlayASound(playerHitSound);
    
            if(animator != null)
                animator.SetBool(PlayerHit, true);
            if (particleSystem != null)
            {
                particleSystem.Play();
                PlayAdditionalEffects();
            }

            isHitPlayer = true;
            
            DisableGameObject();
        }

        protected virtual void PlayAdditionalEffects()
        {
            
        }

        protected virtual void DisableGameObject()
        {
            enabled = false;
        }
    }
}