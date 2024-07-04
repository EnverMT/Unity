using UnityEngine;

public abstract class BaseAtributUI : MonoBehaviour
{
    [SerializeField] protected BaseAttribut Attribute;

    protected virtual void OnEnable()
    {
        Attribute.ValueChanged += OnValueChanged;
    }

    protected virtual void OnDisable()
    {
        Attribute.ValueChanged -= OnValueChanged;
    }

    protected abstract void OnValueChanged(IAttribute attribute);
}
