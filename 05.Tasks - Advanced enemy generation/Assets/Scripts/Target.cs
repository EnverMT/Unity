using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Target : MonoBehaviour
{
    private const float MinDistance = 0.1f;

    [SerializeField] private float _speed;
    [SerializeField] private Vector3[] _path;

    private int _pathIndex;

    private void OnValidate()
    {
        if (_path.Length < 2)
            throw new UnityException("Path should have at least two point");
    }

    private void Update()
    {
        Vector3 desiredPosition = GetDesiredPosition();
        Vector3 direction = (desiredPosition - gameObject.transform.position).normalized;

        gameObject.transform.Translate(direction * _speed * Time.deltaTime);
    }

    private Vector3 GetDesiredPosition()
    {
        float distance = Vector3.Distance(gameObject.transform.position, _path[_pathIndex]);
        
        if (distance < MinDistance)
        {
            _pathIndex++;
            if (_pathIndex >= _path.Length)
                _pathIndex = 0;
        }

        return _path[_pathIndex];
    }

}
