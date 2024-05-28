using Assets.Scripts.Base;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Unit))]
public class PlayerAnimation : MonoBehaviour
{
    private const string ParamHorizontalSpeed = "HorizontalSpeed";
    private const string ParamIsGrounded = "IsGrounded";
    private const string ParamVerticalSpeed = "VerticalSpeed";

    private Rigidbody2D _body;
    private Animator _animator;
    private Mover _mover;
    private Unit _unit;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _unit = GetComponent<Unit>();
    }

    private void Update()
    {
        _animator.SetFloat(ParamHorizontalSpeed, Mathf.Abs(_body.velocity.x));
        _animator.SetFloat(ParamVerticalSpeed, _body.velocity.y);
        _animator.SetBool(ParamIsGrounded, _unit.CanJump);
    }
}
