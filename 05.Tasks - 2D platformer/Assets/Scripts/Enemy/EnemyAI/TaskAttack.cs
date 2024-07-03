using BehaviorTree;
using UnityEngine;


public class TaskAttack : Node
{
    private readonly Animator _animator;

    private Rigidbody2D _rb;
    private CapsuleCollider2D _lastTarget;
    private Health _targetUnitHealth;

    private float _attackTime;
    private float _attackCounter = 0f;
    private uint _attackDamage;

    public TaskAttack(Rigidbody2D rigidbody2D, Animator animator, float attackTime, uint attackDamage)
    {
        _rb = rigidbody2D;
        _animator = animator;
        _attackTime = attackTime;
        _attackDamage = attackDamage;
    }

    public override NodeState Evaluate()
    {
        CapsuleCollider2D target = GetData(Data.TARGET) as CapsuleCollider2D;

        if (target != _lastTarget)
        {
            _lastTarget = target;
            _targetUnitHealth = target.GetComponent<Health>();
        }

        _attackCounter += Time.deltaTime;
        _rb.velocity = Vector3.zero;

        if (_attackCounter > _attackTime)
        {
            _targetUnitHealth.TakeDamage(_attackDamage);
            _animator.SetTrigger(Params.Attack.Attacking);

            if (!_targetUnitHealth.IsAlive)
                ClearData(Data.TARGET);
            else
                _attackCounter = 0f;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
