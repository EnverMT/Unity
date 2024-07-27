using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Platformer.Base
{
    public class BaseAttack : MonoBehaviour
    {
        public float AttackCooldown;
        public float Damage;
        public float Range;

        protected float LastAttackedTime;

        public bool CanAttack => Time.realtimeSinceStartup - LastAttackedTime >= AttackCooldown;

        public event Action Attacked;


        public virtual void ApplyAttack(BaseUnit[] units)
        {
            if (!CanAttack)
                return;

            LastAttackedTime = Time.realtimeSinceStartup;
            Attacked?.Invoke();

            foreach (BaseUnit unit in units)
            {
                unit.Health.Decrease(Damage);
            }
        }

        public virtual void ApplyAttack(BaseUnit unit)
        {
            if (!CanAttack)
                return;

            LastAttackedTime = Time.realtimeSinceStartup;
            Attacked?.Invoke();
            unit.Health.Decrease(Damage);
        }

        public virtual bool IsInAttackRange(BaseUnit target)
        {
            float distance = Vector2.Distance(gameObject.transform.position, target.gameObject.transform.position);

            if (distance <= Range)
                return true;

            return false;
        }

        public virtual T[] GetUnitsInAttackRange<T>(BaseUnit unit) where T : BaseUnit
        {
            List<BaseUnit> units = new();
            RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, unit.BaseMovement.Direction.normalized * Range);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out BaseUnit enemy))
                    units.Add(enemy);
            }

            return units.OfType<T>().ToArray();
        }
    }
}
