using BehaviorTree;
using UnityEngine;


public class TaskPatrol : Node
{
    private const float MinDistance = 0.01f;

    private readonly Transform[] _waypoints;
    private readonly Rigidbody2D _body;

    private int _currentWaypointIndex = 0;
    private readonly float _speed;

    public TaskPatrol(Rigidbody2D rigidbody2D, Transform[] waypoints, float speed)
    {
        _body = rigidbody2D;
        _waypoints = waypoints;
        _speed = speed;
    }

    public override NodeState Evaluate()
    {
        Transform wp = _waypoints[_currentWaypointIndex];
        Vector2 direction = (wp.transform.position - _body.transform.position).normalized;
        _body.velocity = new Vector2(Mathf.Sign(direction.x), _body.velocity.y);


        if ((wp.position.x - _body.transform.position.x) < MinDistance)
        {
            Debug.Log("wp changed");
            _body.transform.position = wp.position;
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
