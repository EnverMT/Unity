using Assets.Scripts.Base;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BaseUnit))]
public class AnimationHandler : MonoBehaviour
{
    private const string HozirontalAxis = "Horizontal";

    private const string ParamHorizontalSpeed = "HorizontalSpeed";
    private const string ParamVerticalSpeed = "VerticalSpeed";
    private const string ParamOnGround = "OnGround";

    private Rigidbody2D _body;
    private Animator _animator;
    private BaseUnit _unit;

    private float _axisRawInput;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _unit = GetComponent<BaseUnit>();
    }

    private void Update()
    {
        _animator.SetFloat(ParamHorizontalSpeed, Mathf.Abs(_body.velocity.x));
        _animator.SetFloat(ParamVerticalSpeed, _body.velocity.y);
        _animator.SetBool(ParamOnGround, _unit.OnGround);

        _axisRawInput = Input.GetAxisRaw(HozirontalAxis);
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_axisRawInput) > 0)
            FlipHorizontally(_axisRawInput);
    }

    private void FlipHorizontally(float axisRawDirection)
    {
        Vector2 scale = transform.localScale;
        scale.x = axisRawDirection * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
