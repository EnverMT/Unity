using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerAttack : BaseAttack
{
    private readonly int _attackMouseButton = 0;
    private bool _attackInput = false;

    private PlayerMover _playerMover;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        _attackInput = Input.GetMouseButtonDown(_attackMouseButton);
    }

    private void FixedUpdate()
    {
        if (_attackInput)
        {
            Enemy[] enemies = GetUnitsInAttackRange<Enemy>();

            if (enemies.Length > 0 && CanAttack)
                DealDamage(enemies);
        }
    }

    public override T[] GetUnitsInAttackRange<T>()
    {
        List<T> units = new();
        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, _playerMover.Direction * Range);

        foreach (RaycastHit2D hit in hits)
            if (hit.collider.gameObject.TryGetComponent(out T enemy))
                units.Add(enemy);

        return units.ToArray();
    }
}