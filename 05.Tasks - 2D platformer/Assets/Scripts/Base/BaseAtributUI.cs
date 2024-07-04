using UnityEngine;

public abstract class BaseAtributUI : MonoBehaviour
{
    [SerializeField] protected IAtribute atribute { get; private set; }

    private void OnEnable()
    {
        atribute.ValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        atribute.ValueChanged -= OnValueChanged;
    }

    protected abstract void OnValueChanged(IAtribute atribute);
}
