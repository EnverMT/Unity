using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Base
{
    public abstract class BaseUnit : MonoBehaviour
    {
        [SerializeField] public virtual bool HasJumpAbility { get; protected set; } = false;
        [SerializeField] private float _health;

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

        public bool TakeHit(float damage)
        {
            _health -= damage;
            Debug.Log($"Take hit. HP={_health}");

            bool isDead = _health <= 0;
            if (isDead)
                Die();
            return isDead;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}