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
        if (Ability.IsChanneling)
        {
            _slider.value = Ability.RemainingChannelTime / Ability.ChannelTime;
            return;
        }

        if (Ability.IsCooldowning)
            _slider.value = (Ability.Cooldown - Ability.RemainingCooldown) / Ability.Cooldown;
    }
}
