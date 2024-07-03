using UnityEngine;


[RequireComponent(typeof(Health))]
public abstract class BaseUnit : MonoBehaviour
{
    public Health Health;


    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
    }

    protected virtual void Update()
    {
        if (!Health.IsAlive)
            Destroy(gameObject);
    }
}
