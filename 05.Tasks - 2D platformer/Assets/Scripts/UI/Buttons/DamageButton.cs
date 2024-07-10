public class DamageButton : BaseButton
{
    protected override void Clicked()
    {
        _unit.TakeDamage(10);
    }
}
