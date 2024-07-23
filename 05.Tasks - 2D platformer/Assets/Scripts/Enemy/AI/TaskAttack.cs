using BehaviorTree;


namespace Platformer.Enemy.AI
{
    public class TaskAttack : Node
    {
        public override NodeState Evaluate()
        {
            if (Context.target == null)
            {
                state = NodeState.FAILURE;
                return state;
            }

            Context.unit.BaseMovement.Stop();

            if (Context.unit.Attack.CanAttack)
                Context.unit.Attack.ApplyAttack(Context.target);

            if (!Context.target.Health.IsAlive)
                Context.target = null;

            state = NodeState.RUNNING;
            return state;
        }
    }
}