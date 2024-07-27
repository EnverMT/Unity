using UnityEngine;

namespace Platformer.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] private KeyCode _abilityKeyCode = KeyCode.E;
        [SerializeField] private KeyCode _attackKeyCode = KeyCode.Mouse0;
        [SerializeField] private KeyCode _jumpKeyCode = KeyCode.Space;

        private bool _isAbility = false;
        private bool _isAttack = false;
        private bool _isJump = false;

        private float _axisInput;

        public float Direction { get; private set; }

        public bool GetIsAbility() => GetBoolAsTrigger(ref _isAbility);
        public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);
        public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);


        private void Update()
        {
            Direction = Input.GetAxis(Params.Axis.Horizontal);

            if (Input.GetKeyDown(_jumpKeyCode))
                _isJump = true;

            if (Input.GetKeyDown(_abilityKeyCode))
                _isAbility = true;

            if (Input.GetKeyDown(_attackKeyCode))
                _isAttack = true;
        }

        private bool GetBoolAsTrigger(ref bool value)
        {
            bool localValue = value;
            value = false;
            return localValue;
        }
    }
}