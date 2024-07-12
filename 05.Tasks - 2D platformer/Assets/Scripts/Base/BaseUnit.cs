using UnityEngine;


[RequireComponent(typeof(BaseAttack))]
[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(HealthAttribute))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseUnit : MonoBehaviour
{
    public BaseAttack Attack;
    public BaseMovement BaseMovement;
    public HealthAttribute Health;
    public Rigidbody2D Rigidbody2D;

    protected virtual void Awake()
    {
        Attack = GetComponent<BaseAttack>();
        Health = GetComponent<HealthAttribute>();
        BaseMovement = GetComponent<BaseMovement>();
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