using UnityEngine;


[RequireComponent(typeof(HealthAttribute))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseUnit : MonoBehaviour
{
    public HealthAttribute Health;
    public PlayerAttack Attack;
    public Rigidbody2D Rigidbody2D;

    public Vector2 Direction { get; private set; }

    protected virtual void Awake()
    {
        Health = GetComponent<HealthAttribute>();
        Attack = GetComponent<PlayerAttack>();
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