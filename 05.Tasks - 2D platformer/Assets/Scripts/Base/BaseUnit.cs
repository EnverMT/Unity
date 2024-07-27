using Platformer.Attribute;
using UnityEngine;



namespace Platformer.Base
{
    [RequireComponent(typeof(BaseAttack))]
    [RequireComponent(typeof(BaseMovement))]
    [RequireComponent(typeof(HealthAttribute))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseUnit : MonoBehaviour
    {
        [HideInInspector] public BaseAttack Attack;
        [HideInInspector] public BaseMovement BaseMovement;
        [HideInInspector] public HealthAttribute Health;
        [HideInInspector] public Rigidbody2D Rigidbody2D;

        public abstract int TeamId { get; protected set; }
        public bool IsEnemy(BaseUnit unit) => TeamId != unit.TeamId;


        protected virtual void Awake()
        {
            Attack = GetComponent<BaseAttack>();
            BaseMovement = GetComponent<BaseMovement>();
            Health = GetComponent<HealthAttribute>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnEnable()
        {
            Health.Died += OnDie;
        }

        protected virtual void OnDisable()
        {
            Health.Died -= OnDie;
        }

        protected virtual void OnDie(IAttribute<float> _)
        {
            Destroy(gameObject);
        }
    }
}