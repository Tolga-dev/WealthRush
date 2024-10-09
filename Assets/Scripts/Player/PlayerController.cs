using System;
using Managers;
using Save.GameObjects.Prizes;
using Save.GameObjects.Road;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Managers")] 
        public GameManager gameManager;
        
        [Header("Components")]
        public CharacterController controller;
        
        [Header("Controllers")]
        public InputController inputController;
        public PileController pileController;
        public PlayerAnimationController animationController;
        
        [Header("Parameters")]
        public float xSpeed = 15f;
        public float zSpeed = 10f;

        private float _minBorder;
        private float _maxBorder;

        private void Start()
        {
            _minBorder = gameManager.targetA.position.x;
            _maxBorder = gameManager.targetB.position.x;
        }

        public void UpdatePlayer()
        {
            inputController.HandleMouseInput();
            
            MovePlayer();
        }
        
        private void MovePlayer()
        {
            if (inputController.canMove == false) return;
            
            var moveX = controller.transform.right * (inputController.IsMouseX() * xSpeed);
            var moveZ = controller.transform.forward * zSpeed;
            var move = moveX + moveZ;
            
            controller.Move(move * Time.deltaTime);

            ClampPosition();
        }

        private void ClampPosition()
        {
            var clampedPosition = controller.transform.localPosition;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, _minBorder, _maxBorder);
            controller.transform.localPosition = clampedPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                pileController.AddPrizeToPile(other.gameObject);
                animationController.SetPlayerHolding();
            }
            
        }

        public void ResetPlayer()
        {
            animationController.Reset();
            ResetPos();
        }

        public void ResetPos()
        {
            var initPos = gameManager.playingState.playerInitialPosition;
            transform.position = initPos.position;
        }

        public void StartRunning()
        {
            animationController.StartRunner();
        }

        public void SetWin()
        {
            gameManager.playingState.playerWon = true;
            animationController.StartWinner();
        }
    }
} 