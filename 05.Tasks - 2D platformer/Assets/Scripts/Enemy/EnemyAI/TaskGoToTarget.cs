using BehaviorTree;

public class TaskGoToTarget : Node
{
    public override NodeState Evaluate()
    {
        if (Context.target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Context.unit.Patrol.StopPatrol();
        Context.unit.BaseMovement.HeadTo(Context.target.transform.position);

        state = NodeState.RUNNING;
        return state;
    }
}