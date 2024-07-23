using Platformer.Base;
using Platformer.Enemy;
using UnityEngine;

namespace Platformer.Player
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerAttack : BaseAttack
    {
        [SerializeField] protected float RemainingCooldown;

        private readonly int _attackMouseButton = 0;

        private void Update()
        {
            RemainingCooldown = Mathf.Clamp(AttackCooldown - (Time.realtimeSinceStartup - LastAttackedTime), 0, float.MaxValue);

            if (Input.GetMouseButtonDown(_attackMouseButton))
            {
                EnemyUnit[] enemies = GetUnitsInAttackRange<EnemyUnit>();

                ApplyAttack(enemies);
            }
        }
    }
}