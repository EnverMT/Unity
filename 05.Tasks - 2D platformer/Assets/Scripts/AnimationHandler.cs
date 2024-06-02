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

    [SerializeField] private bool _isFacingRight = true;
    [SerializeField] private bool _flipOnInput = false;

    private Rigidbody2D _body;
    private Animator _animator;
    private BaseUnit _unit;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _unit = GetComponent<BaseUnit>();
    }

    private void Update()
    {
        _animator.SetFloat(ParamHorizontalSpeed, Mathf.Abs(_body.velocity.x));

        if (_unit.HasJumpAbility)
        {
            _animator.SetFloat(ParamVerticalSpeed, _body.velocity.y);
            _animator.SetBool(ParamOnGround, _unit.OnGround);
        }
    }

    private void FixedUpdate()
    {
        if (ShouldFlip())
            FlipHorizontally();
    }

    private void FlipHorizontally()
    {
        _isFacingRight = !_isFacingRight;

        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool ShouldFlip()
    {
        float axis = _flipOnInput ? Input.GetAxisRaw(HozirontalAxis) : _body.velocity.x;

        if (_isFacingRight && axis < 0)
            return true;

        if (!_isFacingRight && axis > 0)
            return true;

        return false;
    }
}
