using UnityEngine;

[RequireComponent(typeof(Patrol))]
public class Enemy : BaseUnit
{
    [HideInInspector] public Patrol Patrol;

    protected override void Awake()
    {
        base.Awake();

        Patrol = GetComponent<Patrol>();
    }
}
