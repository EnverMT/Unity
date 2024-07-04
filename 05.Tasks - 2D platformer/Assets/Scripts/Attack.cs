using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public uint Damage;
    public float Range;
    public float AttackDelay;

    private float _lastAttackTime;

    public event Action<Attack> Attacked;

    public bool CanAttack => Time.time - _lastAttackTime > AttackDelay;

    public void AttackTarget(BaseUnit target)
    {
        if (!CanAttack)
            return;

        _lastAttackTime = Time.time;
        target.Health.TakeDamage(Damage);
        Attacked?.Invoke(this);
    }
}
