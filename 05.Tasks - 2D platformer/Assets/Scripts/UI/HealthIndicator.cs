using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _healthText;

    private void OnValidate()
    {
        Assert.IsNotNull(_healthText);
        Assert.IsNotNull(_health);
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(Health health)
    {
        _healthText.text = $"Health = {health.CurrentHP} / {health.MaxHP}";
    }
}
