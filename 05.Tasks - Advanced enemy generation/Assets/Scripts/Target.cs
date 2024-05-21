using UnityEngine;

public class Target : MonoBehaviour
{
    private const float MinDistance = 0.1f;

    [SerializeField] private float _speed;
    [SerializeField] private Waypoint[] _waypoints;

    private int _waypointIndex = 0;

    private void Update()
    {
        Vector3 direction = (GetWaypointPosition() - gameObject.transform.position).normalized;

        gameObject.transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }

    private Vector3 GetWaypointPosition()
    {
        float distance = Vector3.Distance(gameObject.transform.position, _waypoints[_waypointIndex].transform.position);

        if (distance < MinDistance)
        {
            _waypointIndex++;
            if (_waypointIndex >= _waypoints.Length)
                _waypointIndex = 0;
        }

        return _waypoints[_waypointIndex].transform.position;
    }

}
