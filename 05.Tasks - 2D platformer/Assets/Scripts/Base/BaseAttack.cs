using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    public float AttackCooldown;
    public float Damage;
    public float Range;

    protected float _lastAttackedTime;

    public bool CanAttack => Time.time - _lastAttackedTime > AttackCooldown;

    public event Action<BaseAttack, BaseUnit> Attacked;

    public virtual void DealDamage(BaseUnit target)
    {
        _lastAttackedTime = Time.time;
        target.Health.ChangeValue(-Damage);

        Attacked?.Invoke(this, target);
    }

    public virtual void DealDamage(BaseUnit[] targets)
    {
        foreach (BaseUnit unit in targets)
            DealDamage(unit);
    }

    public virtual bool IsInAttackRange(BaseUnit target)
    {
        float distance = Vector2.Distance(gameObject.transform.position, target.gameObject.transform.position);

        if (distance <= Range)
            return true;

        return false;
    }

    public virtual T[] GetUnitsInAttackRange<T>()
    {
        List<T> units = new();
        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, _mover.Direction * Range);

        foreach (RaycastHit2D hit in hits)
            if (hit.collider.gameObject.TryGetComponent(out T enemy))
                units.Add(enemy);

        return units.ToArray();
    }
}