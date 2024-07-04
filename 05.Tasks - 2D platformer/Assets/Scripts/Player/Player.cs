using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Player : BaseUnit
{
    [SerializeField] private int _attackMouseButton = 0;

    private bool _attackInput = false;
    private Mover _mover;

    protected override void Awake()
    {
        base.Awake();

        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        _attackInput = Input.GetMouseButtonDown(_attackMouseButton);
    }

    private void FixedUpdate()
    {
        if (_attackInput)
        {
            Enemy[] enemies = GetEnemies();

            if (enemies.Length > 0 && Attack.CanAttack)
                AttackEnemy(enemies);
        }
    }

    private Enemy[] GetEnemies()
    {
        List<Enemy> enemies = new List<Enemy>();
        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, _mover.Direction * Attack.Range);

        foreach (RaycastHit2D hit in hits)
            if (hit.collider.gameObject.TryGetComponent(out Enemy enemy))
                enemies.Add(enemy);

        return enemies.ToArray();
    }

    private void AttackEnemy(Enemy[] enemies)
    {
        foreach (Enemy enemy in enemies)
            Attack.AttackTarget(enemy);
    }
}
