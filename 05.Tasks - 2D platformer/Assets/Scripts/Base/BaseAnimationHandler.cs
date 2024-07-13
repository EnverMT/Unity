using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BaseAttack))]
[RequireComponent(typeof(BaseUnit))]
[RequireComponent(typeof(Rigidbody2D))]
public class BaseAnimationHandler : MonoBehaviour
{
    [SerializeField] protected bool _isFacingRight = true;
    [SerializeField] private GameObject _flipableObject;

    protected Animator _animator;
    protected BaseAttack _attack;
    protected BaseUnit _baseUnit;
    protected Rigidbody2D _rbody;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _attack = GetComponent<BaseAttack>();
        _baseUnit = GetComponent<BaseUnit>();
        _rbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _attack.Attacked += AttackAnimation;
    }

    private void OnDisable()
    {
        _attack.Attacked -= AttackAnimation;
    }


    protected virtual void Update()
    {
        _animator.SetFloat(Params.Movement.HorizontalSpeed, Mathf.Abs(_rbody.velocity.x));
    }

    protected virtual void FixedUpdate()
    {
        if (ShouldFlip())
        {
            if (_flipableObject != null)
                FlipHorizontally(_flipableObject);
            else
                FlipHorizontally(gameObject);
        }

    }

    protected virtual void AttackAnimation(BaseAttack attack, BaseUnit target)
    {
        _animator.SetTrigger(Params.Attack.Attacking);
    }

    protected virtual float GetAxis()
    {
        return _rbody.velocity.x;
    }

    protected virtual void FlipHorizontally(GameObject _object)
    {
        _isFacingRight = !_isFacingRight;

        Vector2 scale = _object.transform.localScale;
        scale.x *= -1;
        _object.transform.localScale = scale;
    }

    protected virtual bool ShouldFlip()
    {
        float axis = GetAxis();

        if (_isFacingRight && axis < -0.1f)
            return true;

        if (!_isFacingRight && axis > 0.1f)
            return true;

        return false;
    }
}