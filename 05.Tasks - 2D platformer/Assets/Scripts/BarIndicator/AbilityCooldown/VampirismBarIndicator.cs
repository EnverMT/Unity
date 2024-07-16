using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class VampirismBarIndicator : BaseAbiilityIndicator<Vampirism>
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public override void OnValueChanged()
    {
        if (_ability.IsChanneling)
        {
            _slider.value = _ability.RemainingChannelTime / _ability.ChannelTime;
            return;
        }

        if (_ability.IsCooldowning)
            _slider.value = (_ability.Cooldown - _ability.RemainingCooldown) / _ability.Cooldown;
    }
}
