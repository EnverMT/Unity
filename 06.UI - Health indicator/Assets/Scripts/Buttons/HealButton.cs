using UnityEngine;

public class HealButton : BaseButton
{
    [SerializeField] private HealthAttribute healthAttribute;
    [SerializeField] private float _heal;


    protected override void OnClicked()
    {
        healthAttribute.ChangeValue(_heal);
    }
}
