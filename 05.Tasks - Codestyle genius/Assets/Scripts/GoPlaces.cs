using UnityEngine;

public class GoPlaces : MonoBehaviour
{
    private const float MinDistance = 0.01f;

    [SerializeField] private float _speed;
    [SerializeField] private Transform _waypointsParent;

    private Transform[] _waypoints;
    private int _waypointIndex;

    private void Start()
    {
        _waypoints = new Transform[_waypointsParent.childCount];

        for (int i = 0; i < _waypointsParent.childCount; i++)
            _waypoints[i] = _waypointsParent.GetChild(i).GetComponent<Transform>();
    }

    private void Update()
    {
        Transform waypoint = _waypoints[_waypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, waypoint.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoint.position) < MinDistance)
            transform.LookAt(GetNextWaypoint());
    }

    private Vector3 GetNextWaypoint()
    {
        _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;

        return _waypoints[_waypointIndex].transform.position;
    }
}