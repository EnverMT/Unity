using UnityEngine;

namespace Assets.Scripts.Base
{
    public abstract class Unit : MonoBehaviour
    {
        public bool CanJump { get; private set; }

        private void OnCollisionStay2D(Collision2D collision)
        {
            CanJump = true;
        }

        public void Jumped()
        {
            CanJump = false;
        }
    }
}