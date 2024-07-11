using UnityEngine;
using UnityEngine.Assertions;

public abstract class BaseHealthUI : MonoBehaviour
{
    [SerializeField] protected HealthAttribute _healthAttribute;

    private bool _isAttributeObserving;

    private void OnValidate()
    {
        Assert.IsNotNull(_healthAttribute);
    }

    protected virtual void OnEnable()
    {
        _healthAttribute.ValueChanged += OnValueChanged;
        _healthAttribute.Died += OnDied;

        OnValueChanged(_healthAttribute);

        _isAttributeObserving = true;
    }

    protected virtual void OnDisable()
    {
        if (_isAttributeObserving)
        {
            _healthAttribute.ValueChanged -= OnValueChanged;
            _healthAttribute.Died -= OnDied;
        }

        _isAttributeObserving = false;
    }

    protected virtual void OnDied(HealthAttribute attribute)
    {
        OnDisable();
    }

    protected abstract void OnValueChanged(IAttribute<float> attribute);
}