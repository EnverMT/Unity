using UnityEngine;

[RequireComponent(typeof(BaseUnit))]
public class Patrol : MonoBehaviour
{
    [SerializeField, Range(1f, 100f)] private float _enemySearchDistance;
    [SerializeField] private Waypoint[] _waypoints;

    private BaseUnit _unit;
    private int _waypointIndex = 0;

    public Waypoint TargetWaypoint => _waypoints[_waypointIndex];

    private void Awake()
    {
        _unit = GetComponent<BaseUnit>();
    }

    private void Update()
    {
        _unit.BaseMovement.HeadTo(TargetWaypoint.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Waypoint waypoint) && TargetWaypoint == waypoint)
            ChangeWaypoint();
    }

    public bool IsUnitInFOV(BaseUnit target)
    {
        return Vector3.Distance(_unit.gameObject.transform.position, target.gameObject.transform.position) < _enemySearchDistance;
    }

    private void ChangeWaypoint()
    {
        _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;
    }
}
