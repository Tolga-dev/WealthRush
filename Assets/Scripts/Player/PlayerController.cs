using System;
using Prizes;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterController controller;
        public InputController inputController;
        public PileController pileController;
        
        public float xSpeed = 15f;
        public float zSpeed = 10f;
        
        private void Update()
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
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -3.4f, 3.4f);
            controller.transform.localPosition = clampedPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                pileController.AddPrizeToPile(other.gameObject);
            }
        }
    }
} 