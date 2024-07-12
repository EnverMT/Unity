using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MedKit : BaseCollectable
{
    [SerializeField, Range(0, 1000f)] private float _value;

    protected override void Collected(BaseUnit unit)
    {
        if (unit is Player)
        {
            unit.Health.ChangeValue(_value);
            Destroy(gameObject);
        }
    }
}
