using Assets.Scripts.Base;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BaseUnit))]
public class BaseAnimationHandler : MonoBehaviour
{
    protected const string HozirontalAxis = "Horizontal";
    protected const string ParamHorizontalSpeed = "HorizontalSpeed";

    [SerializeField] protected bool _isFacingRight = true;

    protected Rigidbody2D _body;
    protected Animator _animator;
    protected BaseUnit _unit;

    protected virtual void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _unit = GetComponent<BaseUnit>();
    }

    protected virtual void Update()
    {
        _animator.SetFloat(ParamHorizontalSpeed, Mathf.Abs(_body.velocity.x));
    }

    protected virtual void FixedUpdate()
    {
        if (ShouldFlip())
            FlipHorizontally();
    }

    protected virtual float GetAxis()
    {
        return _body.velocity.x;
    }

    protected virtual void FlipHorizontally()
    {
        _isFacingRight = !_isFacingRight;

        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    protected virtual bool ShouldFlip()
    {
        float axis = GetAxis();

        if (_isFacingRight && axis < 0)
            return true;

        if (!_isFacingRight && axis > 0)
            return true;

        return false;
    }
}