using Assets.Scripts.Base;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BaseUnit))]
public class Mover : MonoBehaviour
{
    private const string HozirontalAxis = "Horizontal";

    [Header("Movement")]
    [SerializeField, Range(0, 10f)] private float _speed;

    [Header("Jump")]
    [SerializeField] private KeyCode _jumpKeyCode;
    [SerializeField] private float _jumpSpeed;

    private Rigidbody2D _body;
    private float _axisInput;
    private bool _jumpInput;
    private BaseUnit _unit;

    #region Unity methods
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _unit = GetComponent<BaseUnit>();
    }

    private void Update()
    {
        _jumpInput = Input.GetKey(_jumpKeyCode);
        _axisInput = Input.GetAxis(HozirontalAxis);
    }

    private void FixedUpdate()
    {
        if (_unit.OnGround && _jumpInput)
            Jump();

        if (_axisInput != 0f)
            HorizontalMovement(_axisInput);
    }
    #endregion

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, _jumpSpeed);
        _unit.Jumped();
    }

    private void HorizontalMovement(float axisInput)
    {
        _body.velocity = new Vector2(axisInput * _speed, _body.velocity.y);
    }
}