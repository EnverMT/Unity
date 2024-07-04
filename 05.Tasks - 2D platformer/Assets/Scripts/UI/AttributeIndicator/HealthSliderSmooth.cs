using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderSmooth : BaseAtributeUI
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;
    private float _visibleValue;

    protected override void OnEnable()
    {
        base.OnEnable();

        _visibleValue = Attribute.Value / Attribute.MaxValue;
        _slider.value = _visibleValue;
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
        while (Mathf.Approximately(_visibleValue, target) == false)
        {
            _visibleValue = Mathf.MoveTowards(_visibleValue, target, _speed * Time.deltaTime);
            _slider.value = _visibleValue;

            yield return null;
        }

        _visibleValue = target;
    }
}
