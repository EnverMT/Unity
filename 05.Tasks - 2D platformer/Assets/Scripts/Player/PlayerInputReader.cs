using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        private Dictionary<KeyCode, bool> _abilities = new();

        public float Direction { get; private set; }

        private void Update()
        {
            Direction = Input.GetAxis(Params.Axis.Horizontal);

            foreach (KeyCode key in _abilities.Keys)
            {
                if (Input.GetKeyDown(key))
                    _abilities[key] = true;
            }
        }

        public void InitAbilityKeys(KeyCode[] keys)
        {
            foreach (KeyCode key in keys)
            {
                _abilities.Add(key, false);
            }
        }

        public bool IsGetAbilityKey(KeyCode key)
        {
            if (_abilities.ContainsKey(key) && _abilities[key] == true)
            {
                _abilities[key] = false;
                return true;
            }

            return false;
        }
    }
}