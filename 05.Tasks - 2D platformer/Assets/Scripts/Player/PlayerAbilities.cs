using Platformer.Ability;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;


namespace Platformer.Player
{
    public class PlayerAbilities : MonoBehaviour
    {
        [Header("Vampirism")]
        [SerializeField] private Vampirism _vampirism;
        [SerializeField] private KeyCode _vampirismKey;

        private readonly Dictionary<KeyCode, BaseAbility> _abilitiesCode = new();

        private void OnEnable()
        {
            _abilitiesCode.Add(_vampirismKey, _vampirism);
        }

        private void OnDisable()
        {
            _abilitiesCode.Clear();
        }

        public KeyCode[] GetKeys()
        {
            return _abilitiesCode.Keys.ToArray();
        }

        public bool TryGetAbility(KeyCode key, out BaseAbility ability)
        {
            return _abilitiesCode.TryGetValue(key, out ability);
        }

        public bool TryGetAbility<T>(out BaseAbility ability) where T : BaseAbility
        {
            var result = _abilitiesCode.FirstOrDefault(item => item.Value is T);

            if (result.Value != null)
            {
                ability = result.Value;
                return true;
            }

            ability = null;
            return false;
        }
    }
}