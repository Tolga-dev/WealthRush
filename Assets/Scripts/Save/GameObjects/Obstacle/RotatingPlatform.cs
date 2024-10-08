using UnityEngine;

namespace Save.GameObjects.Obstacle
{
    public class RotatingPlatform : MonoBehaviour
    {
        [SerializeField] float rotatingSpeed = 10f;
        [SerializeField] Vector3 rotatingAxis;

        void Update()
        {
            transform.Rotate(rotatingAxis, rotatingSpeed * Time.deltaTime);
        }
    }
}
