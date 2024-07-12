using UnityEngine;

public class HealButton : BaseButton
{
    [SerializeField, Range(0, 100f)] private float _heal;

    protected override void OnClicked()
    {
        _healthAttribute.ChangeValue(_heal);
    }
}
