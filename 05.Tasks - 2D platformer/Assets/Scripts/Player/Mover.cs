using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BaseUnit))]
public class Mover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Range(0, 10f)] private float _speed;

    [Header("Jump")]
    [SerializeField] private KeyCode _jumpKeyCode;
    [SerializeField] private float _jumpSpeed;

    private Rigidbody2D _body;
    private float _axisInput;
    private bool _jumpInput;
    private Player _unit;

    public Vector2 Direction { get; private set; }

    public bool OnGround { get; private set; }


    #region Unity methods
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _unit = GetComponent<Player>();
    }

    private void Update()
    {
        _jumpInput = Input.GetKey(_jumpKeyCode);
        _axisInput = Input.GetAxis(Params.Axis.Horizontal);
    }

    private void FixedUpdate()
    {
        if (OnGround && _jumpInput)
            Jump();

        if (_axisInput != 0f)
            SetHorizontalVelocity(_axisInput);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out TilemapCollider2D _))
            OnGround = true;
    }
    #endregion

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, _jumpSpeed);
        OnGround = false;
    }

    private void SetHorizontalVelocity(float axisInput)
    {
        _body.velocity = new Vector2(axisInput * _speed, _body.velocity.y);
        Direction = (new Vector2(_body.velocity.x, 0)).normalized;
    }
}