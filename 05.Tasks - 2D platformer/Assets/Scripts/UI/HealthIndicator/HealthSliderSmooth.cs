using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderSmooth : BaseAtributUI
{
    [SerializeField] private Slider slider;
    [SerializeField] private float speed;

    private Coroutine _coroutine;
    private float visibleValue;

    protected override void OnEnable()
    {
        base.OnEnable();

        visibleValue = Attribute.Value / Attribute.MaxValue;
        slider.value = visibleValue;
    }

    protected override void OnValueChanged(IAttribute attribute)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        float target = (float)attribute.Value / (float)attribute.MaxValue;
        _coroutine = StartCoroutine(SmoothChange(target));
    }

    private IEnumerator SmoothChange(float target)
    {
        while (Mathf.Approximately(visibleValue, target) == false)
        {
            visibleValue = Mathf.MoveTowards(visibleValue, target, speed * Time.deltaTime);
            slider.value = visibleValue;

            yield return null;
        }

        visibleValue = target;
    }
}
