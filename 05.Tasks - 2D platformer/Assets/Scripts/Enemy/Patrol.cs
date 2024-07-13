using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseUnit))]
public class Patrol : MonoBehaviour
{
    [SerializeField, Range(1f, 100f)] private float _enemySearchDistance;
    [SerializeField] private Waypoint[] _waypoints;

    private BaseUnit _unit;
    [SerializeField] private int _waypointIndex = 0;
    [SerializeField] private bool _isPatroling;
    [SerializeField] private float _distanceToWP;

    public Waypoint TargetWaypoint => _waypoints[_waypointIndex];

    private void Awake()
    {
        _unit = GetComponent<BaseUnit>();
    }

    private void Update()
    {
        if (_isPatroling)
            _unit.BaseMovement.HeadToHorizontal(TargetWaypoint.transform.position);

        _distanceToWP = (TargetWaypoint.transform.position - gameObject.transform.position).magnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Waypoint wp) && wp == TargetWaypoint)
            ChangeWaypoint();
    }

    public void StartPatrol()
    {
        _isPatroling = true;
    }

    public void StopPatrol()
    {
        _isPatroling = false;
    }

    public bool IsUnitInFOV(BaseUnit target)
    {
        return Vector3.Distance(gameObject.transform.position, target.gameObject.transform.position) < _enemySearchDistance;
    }

    public T[] GetUnitsInFOV<T>() where T : BaseUnit
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, _enemySearchDistance);

        List<T> units = new();

        foreach (Collider2D c in colliders)
        {
            if (c.gameObject.TryGetComponent(out T unit))
                units.Add(unit);
        }

        return units.OfType<T>().ToArray();
    }

    private void ChangeWaypoint()
    {
        _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;
        Debug.Log($"change wp={_waypointIndex}");
    }
}
