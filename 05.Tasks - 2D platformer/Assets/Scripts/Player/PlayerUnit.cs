using Platformer.Ability;
using Platformer.Base;
using Platformer.Enemy;

using UnityEngine;

namespace Platformer.Player
{
    [RequireComponent(typeof(PlayerInputReader))]
    [RequireComponent(typeof(PlayerAbilities))]
    public class PlayerUnit : BaseUnit
    {
        [HideInInspector] public PlayerAbilities Abilities;
        [HideInInspector] public PlayerInputReader InputReader;

        public override int TeamId { get; protected set; } = 0;

        protected override void Awake()
        {
            base.Awake();

            Abilities = GetComponent<PlayerAbilities>();
            InputReader = GetComponent<PlayerInputReader>();
        }

        private void FixedUpdate()
        {
            if (InputReader.GetIsAbility() == true && Abilities.TryGetAbility(out Vampirism ability))
                ability.Execute(this);

            if (Attack.CanAttack && InputReader.GetIsAttack() == true)
            {
                BaseUnit[] enemies = Attack.GetUnitsInAttackRange<EnemyUnit>(this);

                Attack.ApplyAttack(enemies);
            }

            if (BaseMovement.OnGround && InputReader.GetIsJump())
                BaseMovement.Jump();

            if (InputReader.Direction != 0)
                BaseMovement.SetHorizontalVelocity(InputReader.Direction);
        }
    }
}