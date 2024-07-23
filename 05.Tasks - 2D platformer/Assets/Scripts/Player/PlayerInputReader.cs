using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] private KeyCode _jumpKeyCode;

        private bool _isJump = false;

        private Dictionary<KeyCode, bool> _abilities;

        public float Direction { get; private set; }
        public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);


        private void Update()
        {
            Direction = Input.GetAxis(Params.Axis.Horizontal);

            if (Input.GetKeyDown(_jumpKeyCode))
                _isJump = true;

            _abilities.Where(item => Input.GetKeyDown(item.Key))
                .Select(item => _abilities[item.Key] = true);
        }

        public void InitAbilityKeys(KeyCode[] keys)
        {
            _abilities = keys.ToDictionary(key => key, key => false);
        }

        public bool IsGetAbilityKey(KeyCode key)
        {
            return _abilities.ContainsKey(key) && _abilities[key] && (_abilities[key] = false);
        }

        private bool GetBoolAsTrigger(ref bool value)
        {
            bool localValue = value;
            value = false;
            return localValue;
        }
    }
}