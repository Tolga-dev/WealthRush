using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] float rotatingSpeed = 10f;
    [SerializeField] Vector3 rotatingAxis;

    void Update()
    {
        transform.Rotate(rotatingAxis, rotatingSpeed * Time.deltaTime);
    }
}
