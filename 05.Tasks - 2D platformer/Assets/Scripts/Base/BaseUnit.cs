using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Base
{
    public abstract class BaseUnit : MonoBehaviour
    {
        [SerializeField] public virtual bool HasJumpAbility { get; protected set; } = false;
        public bool OnGround { get; private set; }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TilemapCollider2D _))
                OnGround = true;
        }

        public void Jumped()
        {
            OnGround = false;
        }
    }
}