using UnityEngine;

public abstract class BaseHealthUI : MonoBehaviour
{
    [SerializeField] protected HealthAttribute _healthAttribute;

    private bool _isAttributeObserving;

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

    private void OnDied(HealthAttribute _)
    {
        OnDisable();
    }

    protected abstract void OnValueChanged(IAttribute<float> attribute);
}