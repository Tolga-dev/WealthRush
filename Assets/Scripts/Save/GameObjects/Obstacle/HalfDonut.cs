using UnityEngine;

namespace Save.GameObjects.Obstacle
{
    public class HalfDonut : Obstacle
    {
        [SerializeField] float speed = 2f;
        [SerializeField] float minXValue;
        [SerializeField] float maxXValue;
        public Transform stick;

        void Update()
        {
            var vector3 = stick.transform.localPosition;
            vector3.x = Mathf.Lerp(minXValue, maxXValue, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
            stick.transform.localPosition = vector3;
        }

    }
}