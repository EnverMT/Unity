using TMPro;
using UnityEngine;

public class HealthText : BaseAtributeUI
{
    [SerializeField] private TextMeshProUGUI _textPro;

    protected override void OnValueChanged(IAttribute attribute)
    {
        _textPro.text = $"Health = {Attribute.Value} / {Attribute.MaxValue}";
    }
}
