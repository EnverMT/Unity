using Platformer.Ability;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;


namespace Platformer.Player
{
    public class PlayerAbilities : MonoBehaviour
    {
        [SerializeField] private Vampirism _vampirism;

        private readonly Dictionary<KeyCode, BaseAbility> _abilitiesCode = new();

        private void OnEnable()
        {
            _abilitiesCode.Add(KeyCode.E, _vampirism);
        }

        private void OnDisable()
        {
            _abilitiesCode.Clear();
        }

        public bool TryGetAbility(KeyCode key, out BaseAbility ability)
        {
            ability = null;

            if (_abilitiesCode.ContainsKey(key))
            {
                ability = _abilitiesCode[key];
                return true;
            }

            return false;
        }

        public bool TryGetAbility<T>(out BaseAbility ability) where T : BaseAbility
        {
            ability = null;

            if (_abilitiesCode.Any(item => item.Value is T ability))
                return true;

            return false;
        }
    }
}