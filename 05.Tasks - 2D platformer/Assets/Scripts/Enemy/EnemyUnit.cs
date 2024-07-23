using Platformer.Base;
using UnityEngine;

namespace Platformer.Enemy
{
    [RequireComponent(typeof(Patrol))]
    public class EnemyUnit : BaseUnit
    {
        [HideInInspector] public Patrol Patrol;

        protected override void Awake()
        {
            base.Awake();

            Patrol = GetComponent<Patrol>();
        }
    }

}