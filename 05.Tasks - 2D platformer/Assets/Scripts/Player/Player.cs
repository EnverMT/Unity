using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Animator))]
public class Player : BaseUnit
{
    [SerializeField] private int _attackMouseButton = 0;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private uint _attackDamage = 10;


    private bool _attack = false;
    private Mover _mover;
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();

        _mover = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        _attack = Input.GetMouseButtonDown(_attackMouseButton);
    }

    private void FixedUpdate()
    {
        if (_attack)
        {
            _animator.SetTrigger(Params.Attack.Attacking);
            Enemy[] enemies = GetEnemies();

            if (enemies.Length > 0)
                Attack(enemies);
        }
    }

    private Enemy[] GetEnemies()
    {
        List<Enemy> enemies = new List<Enemy>();
        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, _mover.Direction * _attackRange);

        foreach (RaycastHit2D hit in hits)
            if (hit.collider.gameObject.TryGetComponent(out Enemy enemy))
                enemies.Add(enemy);

        return enemies.ToArray();
    }

    private void Attack(Enemy[] enemies)
    {
        foreach (Enemy enemy in enemies)
            enemy.Health.TakeDamage(_attackDamage);
    }
}
