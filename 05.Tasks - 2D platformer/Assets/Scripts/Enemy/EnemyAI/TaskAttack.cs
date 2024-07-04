using BehaviorTree;
using UnityEngine;


public class TaskAttack : Node
{
    private readonly Animator _animator;
    private readonly Attack _attack;
    private readonly Rigidbody2D _rb;

    private CapsuleCollider2D _lastTarget;
    private Player _targetPlayer;

    public TaskAttack(Rigidbody2D rigidbody2D, Animator animator, Attack attack)
    {
        _rb = rigidbody2D;
        _animator = animator;
        _attack = attack;
    }

    public override NodeState Evaluate()
    {
        CapsuleCollider2D target = GetData(Data.TARGET) as CapsuleCollider2D;

        if (target != _lastTarget)
        {
            _lastTarget = target;

            if (target.TryGetComponent(out Player player))
            {
                _targetPlayer = player;
            }
            else
            {
                state = NodeState.FAILURE;
                return state;
            }
        }

        _rb.velocity = Vector3.zero;

        if (_attack.CanAttack)
        {
            _attack.AttackTarget(_targetPlayer);
            _animator.SetTrigger(Params.Attack.Attacking);

            if (!_targetPlayer.Health.IsAlive)
                ClearData(Data.TARGET);
        }

        state = NodeState.RUNNING;
        return state;
    }
}
