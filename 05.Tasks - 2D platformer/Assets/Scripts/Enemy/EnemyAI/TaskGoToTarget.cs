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
        Player target = GetData(Data.TARGET) as Player;

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        _unit.BaseMovement.HeadTo(target.transform.position);

        state = NodeState.RUNNING;
        return state;
    }
}