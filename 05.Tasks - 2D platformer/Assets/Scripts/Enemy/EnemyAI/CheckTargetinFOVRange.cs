using BehaviorTree;
using System.Linq;
using UnityEngine;

public class CheckTargetinFOVRange : Node
{
    public override NodeState Evaluate()
    {
        if (Context.target == null)
        {
            Player player = Context.unit.Patrol.GetUnitsInFOV<Player>().FirstOrDefault();

            if (player != null)
            {
                Context.target = player;

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        float distance = Vector2.Distance(Context.unit.gameObject.transform.position, Context.target.gameObject.transform.position);

        if (!Context.unit.Patrol.IsUnitInFOV(Context.target))
        {
            Context.target = null;

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}

