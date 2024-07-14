using UnityEngine;

[RequireComponent(typeof(BaseAttack))]
[RequireComponent(typeof(BaseUnit))]
[RequireComponent(typeof(Rigidbody2D))]
public class BaseAnimationHandler : MonoBehaviour
{
    [SerializeField] protected bool IsFacingRight = true;
    [SerializeField] protected Animator Animator;
    protected BaseAttack BaseAttack;
    protected BaseUnit BaseUnit;
    protected Rigidbody2D Rbody;

    [SerializeField] private GameObject _flipableObject;

    protected virtual void Awake()
    {
        //_animator = GetComponent<Animator>();
        BaseAttack = GetComponent<BaseAttack>();
        BaseUnit = GetComponent<BaseUnit>();
        Rbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        BaseAttack.Attacked += AttackAnimation;
    }

    private void OnDisable()
    {
        BaseAttack.Attacked -= AttackAnimation;
    }


    protected virtual void Update()
    {
        Animator.SetFloat(Params.Movement.HorizontalSpeed, Mathf.Abs(Rbody.velocity.x));
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

    protected virtual void AttackAnimation(BaseAttack attack)
    {
        Animator.SetTrigger(Params.Attack.Attacking);
    }

    protected virtual float GetAxis()
    {
        return Rbody.velocity.x;
    }

    //protected virtual void FlipHorizontally(GameObject _object)
    //{
    //    _isFacingRight = !_isFacingRight;

    //    Vector2 scale = _object.transform.localScale;
    //    scale.x *= -1;
    //    _object.transform.localScale = scale;
    //}

    protected virtual void FlipHorizontally(GameObject _object)
    {
        IsFacingRight = !IsFacingRight;

        _object.transform.localRotation *= Quaternion.Euler(0, 180, 0);
    }

    protected virtual bool ShouldFlip()
    {
        float axis = GetAxis();

        if (IsFacingRight && axis < -0.1f)
            return true;

        if (!IsFacingRight && axis > 0.1f)
            return true;

        return false;
    }
}