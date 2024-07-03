using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MedKit : BaseCollectable
{
    [SerializeField] public uint Value { get; private set; } = 100;

    protected override void Collected(BaseUnit unit)
    {
        if (unit is Player)
        {
            unit.Health.Heal(Value);
            Destroy(gameObject);
        }
    }
}
