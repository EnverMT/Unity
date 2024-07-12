using BehaviorTree;

public class TaskPatrol : Node
{
    private Enemy _unit;

    public TaskPatrol(Enemy baseUnit)
    {
        _unit = baseUnit;
    }

    public override NodeState Evaluate()
    {
        _unit.Patrol.StartPatrol();

        state = NodeState.RUNNING;
        return state;
    }
}
