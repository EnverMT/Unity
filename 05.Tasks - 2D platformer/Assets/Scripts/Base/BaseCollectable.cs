using UnityEngine;

public abstract class BaseCollectable : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseUnit unit))
            Collected(unit);
    }

    protected abstract void Collected(BaseUnit unit);
}
