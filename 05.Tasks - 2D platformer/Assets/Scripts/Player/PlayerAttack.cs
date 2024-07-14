using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerAttack : BaseAttack
{
    [SerializeField] protected float _remainingCooldown;

    private readonly int _attackMouseButton = 0;

    private void Update()
    {
        _remainingCooldown = Mathf.Clamp(AttackCooldown - (Time.realtimeSinceStartup - _lastAttackedTime), 0, float.MaxValue);

        if (Input.GetMouseButtonDown(_attackMouseButton))
        {
            Enemy[] enemies = GetUnitsInAttackRange<Enemy>();

            Debug.Log($"enemies count = {enemies.Length}");

            ApplyAttack(enemies);
        }
    }
}