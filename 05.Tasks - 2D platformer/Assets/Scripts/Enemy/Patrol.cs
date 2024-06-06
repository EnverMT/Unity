using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waypoints;
    [SerializeField] private int _speed;

    private Waypoint _targetWaypoint;
    private Rigidbody2D _body;
    private int _waypointIndex = 0;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _targetWaypoint = _waypoints[_waypointIndex];
    }

    private void OnValidate()
    {
        Assert.IsTrue(_waypoints.Length >= 2);
    }

    private void Update()
    {
        Vector2 direction = (_targetWaypoint.transform.position - transform.position).normalized;
        _body.velocity = new Vector2(Mathf.Sign(direction.x), _body.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Waypoint waypoint))
            ChangeTarget(waypoint);
    }

    private void ChangeTarget(Waypoint waypoint)
    {
        if (waypoint != _targetWaypoint)
            return;

        _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;
        _targetWaypoint = _waypoints[_waypointIndex];
    }
}
