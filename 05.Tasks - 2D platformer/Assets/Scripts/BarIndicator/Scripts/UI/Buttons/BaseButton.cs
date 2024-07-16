using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class BaseButton : MonoBehaviour
{
    [SerializeField] protected HealthAttribute _healthAttribute;

    private Button _button;

    private void OnValidate()
    {
        Assert.IsNotNull(_healthAttribute);
    }

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
    }

    protected virtual void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    protected virtual void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    protected abstract void OnClicked();
}