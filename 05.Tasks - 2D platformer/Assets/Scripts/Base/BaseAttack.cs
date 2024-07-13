using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseMovement))]
public class BaseAttack : MonoBehaviour
{
    public float AttackCooldown;
    public float Damage;
    public float Range;

    [SerializeField] protected float _remainingCooldown;
    protected float _lastAttackedTime;
    protected BaseMovement _baseMovement;


    public bool CanAttack => Time.time - _lastAttackedTime > AttackCooldown;

    public event Action<BaseAttack, BaseUnit> Attacked; // Damage source, Damage receiver

    private void Awake()
    {
        _baseMovement = GetComponent<BaseMovement>();
    }

    protected virtual void Update()
    {
        if (!CanAttack)
            _remainingCooldown = AttackCooldown - (Time.time - _lastAttackedTime);
    }

    public virtual void DealDamage(BaseUnit[] targets)
    {
        if (!CanAttack)
            return;

        _lastAttackedTime = Time.time;

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
        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, _baseMovement.Direction * Range);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out T enemy))
                units.Add(enemy);
        }

        return units.ToArray();
    }

    private void DealDamage(BaseUnit target)
    {
        target.Health.ChangeValue(-Damage);
        Attacked?.Invoke(this, target);
    }
}