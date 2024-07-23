using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Platformer.Base
{
    [RequireComponent(typeof(BaseMovement))]
    public class BaseAttack : MonoBehaviour
    {
        public float AttackCooldown;
        public float Damage;
        public float Range;

        protected float LastAttackedTime;
        protected BaseMovement BaseMovement;

        public bool CanAttack => Time.realtimeSinceStartup - LastAttackedTime >= AttackCooldown;

        public event Action<BaseAttack> Attacked;

        private void Awake()
        {
            BaseMovement = GetComponent<BaseMovement>();
        }

        public virtual void ApplyAttack(BaseUnit[] units)
        {
            if (!CanAttack)
                return;

            LastAttackedTime = Time.realtimeSinceStartup;
            Attacked?.Invoke(this);

            foreach (BaseUnit unit in units)
            {
                DealDamage(unit);
            }
        }

        public virtual void ApplyAttack(BaseUnit units)
        {
            if (!CanAttack)
                return;

            LastAttackedTime = Time.realtimeSinceStartup;
            Attacked?.Invoke(this);
            DealDamage(units);
        }

        public virtual bool IsInAttackRange(BaseUnit target)
        {
            float distance = Vector2.Distance(gameObject.transform.position, target.gameObject.transform.position);

            if (distance <= Range)
                return true;

            return false;
        }

        public virtual T[] GetUnitsInAttackRange<T>() where T : BaseUnit
        {
            List<BaseUnit> units = new();
            RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, BaseMovement.Direction.normalized * Range);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out BaseUnit enemy))
                    units.Add(enemy);
            }

            return units.OfType<T>().ToArray();
        }

        private void DealDamage(BaseUnit target)
        {
            target.Health.Decrease(Damage);
        }
    }
}
