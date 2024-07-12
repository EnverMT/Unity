using BehaviorTree;
using UnityEngine;

public class TaskPatrol : Node
{
    private const float MinDistance = 0.1f;

    private readonly Transform[] _waypoints;
    private readonly Rigidbody2D _body;
    private readonly float _speed;
    private int _currentWaypointIndex = 0;

    private BaseUnit _unit;

    public TaskPatrol(BaseUnit baseUnit)
    {
        _body = _unit.Rigidbody2D;
        _waypoints = waypoints;
        _speed = speed;
    }

    public override NodeState Evaluate()
    {
        Transform wp = _waypoints[_currentWaypointIndex];
        Vector2 direction = (wp.transform.position - _body.transform.position).normalized;
        _body.velocity = new Vector2(Mathf.Sign(direction.x) * _speed, _body.velocity.y);

        float distance = Mathf.Abs(wp.position.x - _body.gameObject.transform.position.x);

        if (distance < MinDistance)
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;

        state = NodeState.RUNNING;
        return state;
    }
}
