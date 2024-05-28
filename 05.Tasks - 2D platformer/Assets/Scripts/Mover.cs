using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Mover : MonoBehaviour
{
    private const string HozirontalAxis = "Horizontal";

    [Header("Movement")]
    [SerializeField, Range(0, 10f)] private float _speed;

    [Header("Jump")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpSpeed;    

    private Rigidbody2D _body;
    private float _axisInput;
    private float _axisRawInput;
    private bool _jump;

    public bool IsGrounded { get; private set; }

    #region Unity methods
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        IsGrounded = true;
    }

    private void Update()
    {
        _jump = Input.GetKey(_jumpKey);
        _axisInput = Input.GetAxis(HozirontalAxis);
        _axisRawInput = Input.GetAxisRaw(HozirontalAxis);
    }

    private void FixedUpdate()
    {
        if (IsGrounded && _jump)
            Jump();

        if (_axisInput != 0f)
            HorizontalMovement(_axisInput);

        if (Mathf.Abs(_axisRawInput) > 0)
            FlipHorizontally(_axisRawInput);
    }
    #endregion

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, _jumpSpeed);
        IsGrounded = false;
    }

    private void HorizontalMovement(float axisInput)
    {
        _body.velocity = new Vector2(axisInput * _speed, _body.velocity.y);
    }

    private void FlipHorizontally(float axisRawDirection)
    {
        Vector2 scale = transform.localScale;
        scale.x = axisRawDirection * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}