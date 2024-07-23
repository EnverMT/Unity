using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BarIndicator : MonoBehaviour
{
    [Header("Observing object")]
    [SerializeField] private MonoBehaviour _barIndicatorObject;

    [Header("Smooth bar change")]
    [SerializeField] private bool _isSmooth;
    [SerializeField, Range(0.1f, 1f)] private float _smootSpeed;

    private IBarIndicator _barIndicator;
    private Slider _slider;
    private float _currentValue;

    private void OnValidate()
    {
        Assert.IsTrue(_barIndicatorObject is IBarIndicator barIndicator);
    }

    private void Awake()
    {
        _barIndicator = (IBarIndicator)_barIndicatorObject;
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _currentValue = _barIndicator.InitIndicatorValue;
        _barIndicator.IndicatorValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _barIndicator.IndicatorValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(float value)
    {
        if (_isSmooth)
            StartCoroutine(SmoothChangeTo(value));
        else
            _slider.value = value;
    }

    private IEnumerator SmoothChangeTo(float target)
    {
        while (Mathf.Approximately(_currentValue, target) == false)
        {
            _currentValue = Mathf.MoveTowards(_currentValue, target, _smootSpeed * Time.deltaTime);
            _slider.value = _currentValue;

            yield return null;
        }

        _currentValue = target;
    }
}
