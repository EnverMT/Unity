using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Range(0, 10f)] protected float _speed;

    protected Rigidbody2D _rigidbody;
    private Transform _pursueTarget;

    public bool OnGround { get; protected set; }
    public float Speed => _speed;
    public Vector2 Direction { get; private set; }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out TilemapCollider2D _))
            OnGround = true;
    }

    protected virtual void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public virtual void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public virtual void HeadTo(Vector2 position)
    {
        Vector2 direction = (position - (Vector2)_rigidbody.transform.position).normalized;

        _rigidbody.velocity = direction * Speed;
    }

    public virtual void HeadTo(Transform transform)
    {
        Vector2 direction = ((Vector2)transform.position - (Vector2)transform.position).normalized;

        _rigidbody.velocity = direction * Speed;
    }
}
