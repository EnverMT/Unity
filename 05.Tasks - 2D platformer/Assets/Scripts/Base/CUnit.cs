using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Base
{
    public abstract class CUnit : MonoBehaviour
    {
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