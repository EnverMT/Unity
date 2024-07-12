using BehaviorTree;

public class CheckTargetInAttackRange : Node
{
    public override NodeState Evaluate()
    {
        if (context.target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if (context.unit.Attack.IsInAttackRange(context.target))
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
