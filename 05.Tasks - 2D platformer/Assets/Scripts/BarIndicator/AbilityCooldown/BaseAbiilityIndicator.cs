using UnityEngine;

public abstract class BaseAbiilityIndicator<T> : MonoBehaviour where T : BaseAbility
{
    [SerializeField] protected T _ability;

    protected virtual void OnEnable()
    {
        _ability.ValueChanged += OnValueChanged;
    }

    protected virtual void OnDisable()
    {
        _ability.ValueChanged -= OnValueChanged;
    }

    public abstract void OnValueChanged();
}
