using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class BaseButton : MonoBehaviour
{
    private Button _button;

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