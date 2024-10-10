using UnityEngine;
using UnityEngine.Serialization;

namespace Save.GameObjects.Obstacle
{
    public class LongStick : Obstacle
    {
        [SerializeField] float speed = 2f;
        [SerializeField] float minAngle;
        [SerializeField] float maxAngle;
        public Transform rotationalTransform;
        private void Update()
        {
            float rotation = Mathf.Lerp(minAngle, maxAngle, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
            rotationalTransform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}