using Platformer.Base;
using UnityEngine;

namespace Platformer.Player
{
    [RequireComponent(typeof(PlayerInputReader))]
    [RequireComponent(typeof(PlayerAbilities))]
    public class PlayerUnit : BaseUnit
    {
        [HideInInspector] public PlayerAbilities Abilities;
        [HideInInspector] public PlayerInputReader InputReader;

        protected override void Awake()
        {
            base.Awake();

            Abilities = GetComponent<PlayerAbilities>();
            InputReader = GetComponent<PlayerInputReader>();
        }

        private void FixedUpdate()
        {
            //if (Abilities.TryGetAbility<Vampirism>(out var ability))
            //{
            //    ability.Execute(this);
            //}
        }
    }
}