using System;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Save.GameObjects.Base
{
    public class GameObjectBase : MonoBehaviour
    {
        [Header("Components")]
        public Animator animator;

        public AudioClip playerHitSound;
        [FormerlySerializedAs("particleSystem")] 
        public ParticleSystem hitPlayerEffect;

        public bool isHitPlayer = false;
        private static readonly int PlayerHit = Animator.StringToHash("HitPlayer");

        
        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CallPlayerGotHit(other.gameObject);
            }
        }
        protected virtual void  CallPlayerGotHit(GameObject player)
        {
            GameManager.Instance.PlayASound(playerHitSound);
    
            if(animator != null)
                animator.SetBool(PlayerHit, true);
            if (hitPlayerEffect != null)
            {
                var playerController = player.GetComponent<PlayerController>();
                SetParticlePosition(hitPlayerEffect, playerController.prizeEffectSpawnPoint.transform);
                PlayAdditionalEffects(playerController);
            }

            isHitPlayer = true;
            
            DisableGameObject();
        }

        protected virtual void PlayAdditionalEffects(PlayerController playerController)
        {
            
        }

        protected virtual void DisableGameObject()
        {
            enabled = false;
        }

        protected void SetParticlePosition(ParticleSystem currentParticle, Transform playerPos)
        {
            var particleTransform = currentParticle.transform;
            particleTransform.parent = playerPos;
            particleTransform.transform.localPosition = Vector3.zero;
    
            currentParticle.Play(); // Start particle system

            var main = currentParticle.main;
            Destroy(currentParticle.gameObject, main.duration + main.startLifetime.constantMax);
        }


    }
}