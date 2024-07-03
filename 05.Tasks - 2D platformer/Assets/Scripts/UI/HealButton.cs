public class HealButton : BaseButton
{
    protected override void Clicked()
    {
        _unit.Heal(10);
    }
}