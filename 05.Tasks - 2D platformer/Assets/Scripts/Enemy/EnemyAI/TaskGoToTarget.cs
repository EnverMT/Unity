using BehaviorTree;

public class TaskGoToTarget : Node
{
    public override NodeState Evaluate()
    {
        if (context.target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        context.unit.Patrol.StopPatrol();
        context.unit.BaseMovement.HeadTo(context.target.transform.position);

        state = NodeState.RUNNING;
        return state;
    }
}