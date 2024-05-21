using UnityEngine;

public class GoPlaces : MonoBehaviour
{
    private const float MinDistance = 0.01f;

    [SerializeField] private float _speed;
    [SerializeField] private Transform WaypointCollection;

    private Transform[] waypoints;
    private int _waypointIndex;
    
    private void Start()
    {
        waypoints = new Transform[WaypointCollection.childCount];

        for (int i = 0; i < WaypointCollection.childCount; i++)
        {
            if (WaypointCollection.GetChild(i).TryGetComponent(out Transform transform))
                waypoints[i] = transform;
        }
    }

    private void Update()
    {
        Transform waypoint = waypoints[_waypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, waypoint.position, _speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, waypoint.position) < MinDistance)
            GetNextWaypoint();
    }

    private Vector3 GetNextWaypoint()
    {
        _waypointIndex = (_waypointIndex + 1) % waypoints.Length;
        
        Vector3 nextWaypoint = waypoints[_waypointIndex].transform.position;        
        
        transform.LookAt(nextWaypoint);

        return nextWaypoint;
    }
}