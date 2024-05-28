using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Mover))]
public class PlayerAnimation : MonoBehaviour
{
    private const string ParamHorizontalSpeed = "HorizontalSpeed";
    private const string ParamIsGrounded = "IsGrounded";
    private const string ParamVerticalSpeed = "VerticalSpeed";

    private Rigidbody2D _body;
    private Animator _animator;
    private Mover _mover;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        _animator.SetFloat(ParamHorizontalSpeed, Mathf.Abs(_body.velocity.x));
        _animator.SetFloat(ParamVerticalSpeed, _body.velocity.y);
        _animator.SetBool(ParamIsGrounded, _mover.IsGrounded);
    }
}
