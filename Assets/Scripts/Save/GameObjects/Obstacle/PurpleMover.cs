using Managers;
using UnityEngine;

namespace Save.GameObjects.Obstacle
{
    public class PurpleMover : Obstacle
    {
        private float _startX; // Starting X position
        private float _endX;   // Ending X position

        private float _currentX; // Current X position
        private bool _movingToEndX = true; // Flag to check direction

        public float moveSpeed = 2f; // Speed of movement
        public float rotationSpeed = 100f; // Speed of rotation
        private void Start()
        {
            var gameManager = GameManager.Instance;
            _startX = gameManager.targetA.position.x;
            _endX = gameManager.targetB.position.x;
            
            _currentX = _startX;
            
            transform.position = new Vector3(_currentX, transform.position.y, transform.position.z);
        }

        private void Update()
        {
            MoveAndRotate();
        }

        private void MoveAndRotate()
        {
            var targetX = _movingToEndX ? _endX : _startX;
            
            var position = transform.position;
            var direction = new Vector3(targetX, position.y, position.z) - position;
            var distance = direction.magnitude;

            if (distance > 0.1f)
            {
                direction.Normalize();
                transform.position += direction * (moveSpeed * Time.deltaTime);
            }

            if (distance < 0.1f)
            {
                SwitchDirection();
            }
        }

        private void SwitchDirection()
        {
            _movingToEndX = !_movingToEndX; // Update direction flag
        }
    }
}