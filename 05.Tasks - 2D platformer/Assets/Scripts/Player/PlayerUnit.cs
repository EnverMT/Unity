using Platformer.Ability;
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

        protected override void OnEnable()
        {
            base.OnEnable();

            InputReader.InitAbilityKeys(Abilities.GetKeys());
        }

        private void FixedUpdate()
        {
            foreach (KeyCode key in Abilities.GetKeys())
            {
                if (InputReader.IsGetAbilityKey(key) && Abilities.TryGetAbility(key, out BaseAbility ability))
                    ability.Execute(this);
            }
        }
    }
}