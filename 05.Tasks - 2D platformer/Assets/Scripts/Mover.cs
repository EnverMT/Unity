using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Mover : MonoBehaviour
{
    private const string AnimationSpeed = "Speed";
    private const string HozirontalAxis = "Horizontal";

    [Header("Movement")]
    [SerializeField, Range(0, 10f)] private float _speed;

    [Header("Jump")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private bool _isGrounded;

    private Rigidbody2D _body;
    private Animator _animator;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        _isGrounded = true;
    }

    private void Update()
    {
        Jump();
        HorizontalMovement();
        FlipHorizontally();

        _animator.SetFloat(AnimationSpeed, Mathf.Abs(_body.velocity.x));
    }

    private void Jump()
    {
        if (_isGrounded && Input.GetKey(_jumpKey))
        {
            _body.velocity = Vector2.up * _jumpSpeed;
            _isGrounded = false;
        }
    }

    private void HorizontalMovement()
    {
        float direction = Input.GetAxis(HozirontalAxis);
        _body.velocity = new Vector2(direction * _speed, _body.velocity.y);
    }

    private void FlipHorizontally()
    {
        Vector2 scale = transform.localScale;
        scale.x = Input.GetAxisRaw(HozirontalAxis) * Mathf.Abs(scale.x);

        if (scale.x != 0)
            transform.localScale = scale;
    }
}
