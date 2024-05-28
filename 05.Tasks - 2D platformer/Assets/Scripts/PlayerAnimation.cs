using Assets.Scripts.Base;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CUnit))]
public class PlayerAnimation : MonoBehaviour
{
    private const string ParamHorizontalSpeed = "HorizontalSpeed";
    private const string ParamVerticalSpeed = "VerticalSpeed";

    private const string ParamOnGround = "OnGround";
    private const string ParamJumping = "Jumping";

    private Rigidbody2D _body;
    private Animator _animator;
    private Mover _mover;
    private CUnit _unit;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _unit = GetComponent<CUnit>();
    }

    private void Update()
    {
        _animator.SetFloat(ParamHorizontalSpeed, Mathf.Abs(_body.velocity.x));
        _animator.SetFloat(ParamVerticalSpeed, _body.velocity.y);
        _animator.SetBool(ParamOnGround, _unit.OnGround);
        _animator.SetBool(ParamJumping, _unit.Jumping);
    }
}
