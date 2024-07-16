using UnityEngine;

public abstract class BaseAbiilityIndicator<T> : MonoBehaviour where T : BaseAbility
{
    [SerializeField] protected T Ability;

    protected virtual void OnEnable()
    {
        Ability.ValueChanged += OnValueChanged;
    }

    protected virtual void OnDisable()
    {
        Ability.ValueChanged -= OnValueChanged;
    }

    public abstract void OnValueChanged();
}
