using UnityEngine;


namespace Platformer.Base
{
    [RequireComponent(typeof(BaseAttack))]
    [RequireComponent(typeof(BaseUnit))]
    public class BaseAnimationHandler : MonoBehaviour
    {
        [SerializeField] protected bool IsFacingRight = true;
        [SerializeField] protected Animator Animator;

        protected BaseAttack Attack;
        protected BaseUnit BaseUnit;

        [SerializeField] private GameObject _flipableObject;

        private void Awake()
        {
            Attack = GetComponent<BaseAttack>();
            BaseUnit = GetComponent<BaseUnit>();
        }


        private void OnEnable()
        {
            Attack.Attacked += AttackAnimation;
        }

        private void OnDisable()
        {
            Attack.Attacked -= AttackAnimation;
        }

        protected virtual void Update()
        {
            Animator.SetFloat(Params.Movement.HorizontalSpeed, Mathf.Abs(BaseUnit.Rigidbody2D.linearVelocity.x));
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

        protected virtual void AttackAnimation()
        {
            Animator.SetTrigger(Params.Attack.Attacking);
        }

        protected virtual float GetAxis()
        {
            return BaseUnit.Rigidbody2D.linearVelocity.x;
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
}