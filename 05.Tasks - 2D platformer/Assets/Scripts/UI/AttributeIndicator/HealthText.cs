using TMPro;
using UnityEngine;

public class HealthText : BaseAtributeUI<float>
{
    [SerializeField] private TextMeshProUGUI _textPro;

    protected override void OnValueChanged(IAttribute<float> attribute)
    {
        _textPro.text = $"Health = {Attribute.Value} / {Attribute.MaxValue}";
    }
}
