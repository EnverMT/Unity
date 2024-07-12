using BehaviorTree;
using System.Linq;
using UnityEngine;

public class CheckTargetinFOVRange : Node
{
    private readonly Enemy _unit;

    public CheckTargetinFOVRange(Enemy unit)
    {
        _unit = unit;
    }

    public override NodeState Evaluate()
    {
        Player target = GetData(Data.TARGET) as Player;

        if (target == null)
        {
            Player player = _unit.Patrol.GetUnitsInFOV<Player>().FirstOrDefault();

            if (player != null)
            {
                Parent.Parent.SetData(Data.TARGET, player);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        float distance = Vector2.Distance(_unit.gameObject.transform.position, target.gameObject.transform.position);

        if (!_unit.Patrol.IsUnitInFOV(target))
        {
            ClearData(Data.TARGET);

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}

