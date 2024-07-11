using UnityEngine;

public class HealButton : BaseButton
{
    [SerializeField] private HealthAttribute _healthAttribute;
    [SerializeField] private float _heal;


    protected override void OnClicked()
    {
        _healthAttribute.ChangeValue(_heal);
    }
}
