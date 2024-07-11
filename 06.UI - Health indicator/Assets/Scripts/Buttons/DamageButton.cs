using UnityEngine;

public class DamageButton : BaseButton
{
    [SerializeField] private HealthAttribute healthAttribute;
    [SerializeField] private float _damage;


    protected override void OnClicked()
    {
        healthAttribute.ChangeValue(-_damage);
    }
}
