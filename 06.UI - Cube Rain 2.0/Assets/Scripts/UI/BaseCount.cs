using UnityEngine;

public abstract class BaseCount : MonoBehaviour
{
    [SerializeField] protected Spawner Spawner;

    protected virtual void OnEnable()
    {
        Spawner.ValueChanged += OnValueChanged;
    }

    protected virtual void OnDisable()
    {
        Spawner.ValueChanged -= OnValueChanged;
    }

    protected abstract void OnValueChanged();
}
