using UnityEngine;

public abstract class BaseAtributeUI : MonoBehaviour
{
    [SerializeField] protected BaseAttribute Attribute;

    protected virtual void OnEnable()
    {
        Attribute.ValueChanged += OnValueChanged;
    }

    protected virtual void OnDisable()
    {
        Attribute.ValueChanged -= OnValueChanged;
    }

    protected abstract void OnValueChanged(IAttribute<float> attribute);
}
