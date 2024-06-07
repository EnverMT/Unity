using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : BaseCollectable
{
    protected override void Collected(BaseUnit unit)
    {
        if (unit is Player)
        {
            Destroy(gameObject);
        }
    }
}
