using System.IO;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Vector3[] _path;
    [SerializeField] private float _speed;

    private int _pathIndex = 0;

    private void OnEnable()
    {
        if (_path.Length < 2)
            throw new UnityException("Path should have at least two point");

        gameObject.transform.position = _path[_pathIndex];
    }
    private void Update()
    {
        Vector3 targetPosition = GetTargetPosition();
    }

    private Vector3 GetTargetPosition()
    {
        int index = 0;        

        return _path[index];
    }

}
