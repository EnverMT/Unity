using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Enemy _unit;

    public TaskGoToTarget(Enemy unit)
    {
        _unit = unit;
    }

    public override NodeState Evaluate()
    {
        if (context.target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        _unit.BaseMovement.HeadTo(context.target.transform.position);

        state = NodeState.RUNNING;
        return state;
    }
}