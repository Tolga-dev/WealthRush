using UnityEngine;

namespace Save.GameObjects.Obstacle
{
    public class HalfDonut : Obstacle
    {
        [SerializeField] float speed = 2f;
        [SerializeField] float minXValue;
        [SerializeField] float maxXValue;
        public Transform stick;
        
        private Vector3 _pos1;
        private Vector3 _pos2;
        
        [SerializeField] float rotatingSpeed = 10f;
        [SerializeField] Vector3 rotatingAxis;
        
        void Start()
        {
            var localPosition = transform.localPosition;
            
            _pos1 = new Vector3(minXValue, localPosition.y, localPosition.z);
            _pos2 = new Vector3(maxXValue, localPosition.y, localPosition.z);
        }

        void Update()
        {
            stick.transform.localPosition = Vector3.Lerp(_pos1, _pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f); // sin: [-1, 1], after addition and division: [0, 1]
            transform.Rotate(rotatingAxis, rotatingSpeed * Time.deltaTime);
        }
        
    }
}