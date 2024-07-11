using UnityEngine;

public class DamageButton : BaseButton
{
    [SerializeField, Range(0, 100f)] private float _damage;

    protected override void OnClicked()
    {
        _healthAttribute.ChangeValue(-_damage);
    }
}
