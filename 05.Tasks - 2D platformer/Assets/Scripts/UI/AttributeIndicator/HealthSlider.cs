using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : BaseAtributeUI
{
    [SerializeField] private Slider _slider;
    protected override void OnValueChanged(IAttribute attribute)
    {
        _slider.value = (float)attribute.Value / (float)attribute.MaxValue;
    }
}
