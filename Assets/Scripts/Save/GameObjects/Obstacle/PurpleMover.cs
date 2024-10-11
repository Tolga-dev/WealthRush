using Managers;
using UnityEngine;

namespace Save.GameObjects.Obstacle
{
    public class PurpleMover : Obstacle
    {
        private float _startX; // Starting X position
        private float _endX;   // Ending X position
        private bool _movingToEndX = true; // Direction flag

        public float moveSpeed = 2f; // Speed of movement
        public float smoothness = 0.1f; // Smoothing factor for movement

        private void Start()
        {
            // Get the starting and ending positions from the GameManager
            _startX = gameManager.targetA.position.x;
            _endX = gameManager.targetB.position.x;

            // Initialize position to start point
            SetPosition(_startX);
        }

        private void Update()
        {
            MoveAndRotate();
        }

        private void MoveAndRotate()
        {
            float targetX = _movingToEndX ? _endX : _startX; // Determine the target position

            // Interpolate smoothly between current position and target position
            float newX = Mathf.Lerp(transform.position.x, targetX, smoothness * moveSpeed * Time.deltaTime);

            SetPosition(newX);

            // If the object is very close to the target, switch direction
            if (Mathf.Abs(transform.position.x - targetX) < 0.05f)
            {
                SwitchDirection();
            }
        }

        private void SetPosition(float x)
        {
            // Set the X position while keeping Y and Z the same
            var position = transform.position;
            position.x = x;
            transform.position = position;
        }

        private void SwitchDirection()
        {
            _movingToEndX = !_movingToEndX; // Toggle the direction flag
        }
    }
}