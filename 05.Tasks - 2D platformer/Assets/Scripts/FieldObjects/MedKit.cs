using Platformer.Base;
using UnityEngine;

namespace Platformer.FieldObjects
{


    [RequireComponent(typeof(BoxCollider2D))]
    public class MedKit : BaseCollectable
    {
        [SerializeField, Range(0, 1000f)] private float _value;

        protected override void Collected(BaseUnit unit)
        {
            if (unit is Player.PlayerUnit)
            {
                unit.Health.Increase(_value);
                Destroy(gameObject);
            }
        }
    }
}