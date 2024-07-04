using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : BaseAtributeUI
{
    [SerializeField] private Slider _slider;
    protected override void OnValueChanged(IAttribute<float> attribute)
    {
        _slider.value = attribute.Value / attribute.MaxValue;
    }
}
