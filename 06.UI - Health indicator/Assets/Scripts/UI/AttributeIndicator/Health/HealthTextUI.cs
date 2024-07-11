using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthText : HealthAttributeUI
{
    private TextMeshProUGUI _textPro;

    private void Awake()
    {
        _textPro = GetComponent<TextMeshProUGUI>();
    }

    protected override void OnValueChanged(IAttribute<float> attribute)
    {
        _textPro.text = $"Health = {attribute.Value} / {attribute.MaxValue}";
    }
}
