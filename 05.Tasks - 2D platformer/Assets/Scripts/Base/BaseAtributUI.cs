using UnityEngine;

public abstract class BaseAtributUI : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _monoBeh;

    protected IAttribute Attribute { get; private set; }

    private void Awake()
    {
        Attribute = (IAttribute)_monoBeh;
    }

    private void OnValidate()
    {
        if (_monoBeh == null || _monoBeh is IAttribute)
            return;

        throw new UnityException($"{nameof(_monoBeh)} is not implement Interface: {nameof(Attribute)}");
    }

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
