using UnityEngine;

public class DamageButton : BaseButton
{
    [SerializeField] private HealthAttribute _healthAttribute;
    [SerializeField] private float _damage;


    protected override void OnClicked()
    {
        _healthAttribute.ChangeValue(-_damage);
    }
}
