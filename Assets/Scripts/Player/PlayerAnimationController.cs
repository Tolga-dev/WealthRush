using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerAnimationController
    {
        public Animator animator;
        
        public static readonly int IsRunning = Animator.StringToHash("isRunning");
        public static readonly int IsCarrying = Animator.StringToHash("isCarrying");
        public static readonly int IsDeath = Animator.StringToHash("isDeath");
        public static readonly int IsFinished = Animator.StringToHash("isFinished");

        public void SetAnimation(int animId)
        {
            animator.SetBool(animId, true);
        }

        public void Reset()
        {
            animator.gameObject.SetActive(false);
            animator.gameObject.SetActive(true);
        }

        public void StartRunner()
        {
            SetAnimation(IsRunning);
        }

        public void SetPlayerHolding()
        {
            SetAnimation(IsCarrying);
        }
    }
}