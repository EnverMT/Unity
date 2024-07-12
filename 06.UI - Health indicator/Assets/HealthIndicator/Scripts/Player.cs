using UnityEngine;

[RequireComponent(typeof(HealthAttribute))]
public class Player : MonoBehaviour
{
    private HealthAttribute _healthAttribute;

    private void Awake()
    {
        _healthAttribute = GetComponent<HealthAttribute>();
    }

    private void OnEnable()
    {
        _healthAttribute.Died += OnDied;
    }

    private void OnDisable()
    {
        _healthAttribute.Died -= OnDied;
    }

    private void OnDied(HealthAttribute _)
    {
        OnDisable();
        Destroy(gameObject);
    }
}
