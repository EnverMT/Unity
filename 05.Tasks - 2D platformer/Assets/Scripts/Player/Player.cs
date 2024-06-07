using Assets.Scripts.Base;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Animator))]
public class Player : BaseUnit
{
    [SerializeField] private int _attackMouseButton = 0;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackDamage = 10f;

    private bool _attack = false;
    private Mover _mover;
    private Animator _animator;

    public override bool HasJumpAbility { get => true; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _attack = Input.GetMouseButtonDown(_attackMouseButton);
    }

    private void FixedUpdate()
    {
        if (_attack)
        {
            _animator.SetTrigger(Params.Attack.Attacking);
            Enemy[] enemies = GetEnemies();
            Debug.Log(enemies.Length);

            if (enemies.Length > 0)
                Attack(enemies);
        }
    }

    private Enemy[] GetEnemies()
    {
        Ray ray = new Ray(gameObject.transform.position, _mover.Direction * _attackRange);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        List<Enemy> enemies = new List<Enemy>();

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemies.Add(enemy);
            }
        }

        return enemies.ToArray();
    }

    private void Attack(Enemy[] enemies)
    {
        Debug.Log("attack");
        foreach (Enemy enemy in enemies)
        {
            enemy.TakeDamage(_attackDamage);
        }
    }
}
