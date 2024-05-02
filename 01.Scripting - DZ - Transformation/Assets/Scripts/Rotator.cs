using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0, Space.Self);
    }
}
