using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMover : BaseMovement
{
    [Header("Jump")]
    [SerializeField] private KeyCode _jumpKeyCode;
    [SerializeField] private float _jumpSpeed;

    private float _axisInput;
    private bool _jumpInput;

    protected void Update()
    {
        _jumpInput = Input.GetKey(_jumpKeyCode);
        _axisInput = Input.GetAxis(Params.Axis.Horizontal);
    }

    protected void FixedUpdate()
    {
        if (OnGround && _jumpInput)
            Jump();

        if (_axisInput != 0f)
        {
            SetHorizontalVelocity(_axisInput);
            SetDirection((new Vector2(_rigidbody.velocity.x, 0)).normalized);
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
        OnGround = false;
    }

    private void SetHorizontalVelocity(float axisInput)
    {
        _rigidbody.velocity = new Vector2(axisInput * _speed, _rigidbody.velocity.y);
    }
}