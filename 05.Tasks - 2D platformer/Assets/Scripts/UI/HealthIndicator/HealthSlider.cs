using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : BaseAtributUI
{
    [SerializeField] private Slider slider;
    protected override void OnValueChanged(IAttribute attribute)
    {
        slider.value = (float)attribute.Value / (float)attribute.MaxValue;
    }
}
