using BehaviorTree;

public class TaskPatrol : Node
{
    public override NodeState Evaluate()
    {
        context.unit.Patrol.StartPatrol();

        state = NodeState.RUNNING;
        return state;
    }
}
