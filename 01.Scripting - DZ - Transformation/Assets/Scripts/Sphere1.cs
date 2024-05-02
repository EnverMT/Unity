using UnityEngine;

public class Sphere1 : MonoBehaviour
{
    [SerializeField] private float _speed = 6;

    private readonly Vector3 _startPosition = new Vector3(0, 0, 0);
    private Vector3 _direction = Vector3.forward;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _startPosition);
        bool isOneDirection = _direction.normalized == (transform.position - _startPosition).normalized;

        if (distance > 4 && isOneDirection)
        {
            _direction = -_direction;
        }

        transform.Translate(_speed * _direction * Time.deltaTime);
    }
}
