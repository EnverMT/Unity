using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarUI : BaseHealthUI
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void OnValueChanged(IAttribute<float> attribute)
    {
        _slider.value = attribute.Value / attribute.MaxValue;
    }
}
