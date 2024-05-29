using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waypoints;
    [SerializeField] private int _speed;

    private int _currentPatrolPointIndex = 0;

    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void OnValidate()
    {
        Assert.AreNotEqual(0, _waypoints.Length);
    }

    private void Update()
    {
        Vector2 target = _waypoints[_currentPatrolPointIndex].gameObject.transform.position;

        if (IsTargetBehind(target))
        {
            _currentPatrolPointIndex = GetNextIndex(_waypoints, _currentPatrolPointIndex);
            target = _waypoints[_currentPatrolPointIndex].gameObject.transform.position;
        }

        Vector2 speed = (target - (Vector2)transform.position).normalized * _speed;
        _body.velocity = new Vector2(speed.x, _body.velocity.y);
    }

    private bool IsTargetBehind(Vector2 target)
    {
        return (target - (Vector2)transform.position).normalized.x != _body.velocity.normalized.x;
    }

    private int GetNextIndex(Waypoint[] waypoints, int currentIndex)
    {
        return currentIndex + 1 % waypoints.Length;
    }
}
