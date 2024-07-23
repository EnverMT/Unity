using Platformer.Base;
using Platformer.Player;
using UnityEngine;


namespace Platformer.FieldObjects
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Coin : BaseCollectable
    {
        protected override void Collected(BaseUnit unit)
        {
            if (unit is PlayerUnit)
            {
                Destroy(gameObject);
            }
        }
    }

}