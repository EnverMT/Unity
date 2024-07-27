using UnityEngine;
using UnityEngine.Tilemaps;


namespace Platformer.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BaseMovement : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody;

        [Header("Jump")]
        [SerializeField] private float _jumpSpeed;

        [Header("Movement")]
        [SerializeField, Range(0, 10f)] private float _speed;
        private Transform _pursueTarget;

        public bool OnGround { get; protected set; }

        public float Speed => _speed;

        public Vector2 Direction { get; private set; }


        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TilemapCollider2D _))
                OnGround = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TilemapCollider2D _))
                OnGround = false;
        }


        protected virtual void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public virtual void Stop()
        {
            Rigidbody.linearVelocity = Vector2.zero;
        }

        public virtual void HeadToHorizontal(Vector2 position)
        {
            Vector2 direction = (position - (Vector2)Rigidbody.transform.position).normalized;
            Rigidbody.linearVelocity = new Vector2(Mathf.Sign(direction.x) * Speed, Rigidbody.linearVelocity.y);
        }

        public virtual void SetHorizontalVelocity(float axisInput)
        {
            SetDirection((new Vector2(Rigidbody.linearVelocity.x, 0)).normalized);
            Rigidbody.linearVelocity = new Vector2(axisInput * Speed, Rigidbody.linearVelocity.y);
        }

        public void Jump()
        {
            if (OnGround == false)
                return;

            Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, _jumpSpeed);
        }
    }
}