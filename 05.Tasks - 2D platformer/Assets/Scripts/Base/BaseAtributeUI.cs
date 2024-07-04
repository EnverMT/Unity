using UnityEngine;

public abstract class BaseAtributeUI<T> : MonoBehaviour
{
    [SerializeField] protected BaseAttribute<T> Attribute;

    protected virtual void OnEnable()
    {
        Attribute.ValueChanged += OnValueChanged;
    }

    protected virtual void OnDisable()
    {
        Attribute.ValueChanged -= OnValueChanged;
    }

    protected abstract void OnValueChanged(IAttribute<T> attribute);
}
