using System;
using System.Collections;
using System.Linq;
using UnityEngine;


public class Vampirism : BaseAbility, IChanneling
{
    [Header("Unit")]
    [SerializeField] private BaseUnit _unit;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("AbilityData")]
    [SerializeField] private float _damage;
    [SerializeField] private float _vampirismPercent;
    [SerializeField] private float _radius;
    [SerializeField] private float _duration;

    private float _castStartTime;
    private float _castFinishTime;

    private Coroutine _useAbilityCoroutine;
    private Coroutine _cooldownAbilityCoroutine;


    public override event Action ValueChanged;

    public override KeyCode ActivateKey => KeyCode.E;
    public override bool CanBeCasted => RemainingCooldown == 0f && IsChanneling == false;
    public override float Cooldown { get; protected set; } = 10f;
    public override bool IsCooldowning { get; protected set; } = false;
    public override float RemainingCooldown => Mathf.Clamp(_castFinishTime + Cooldown - Time.realtimeSinceStartup, 0f, float.MaxValue);

    public float Duration => _duration;
    public bool IsChanneling { get; private set; }
    public float RemainingChannelTime => IsChanneling ? Mathf.Clamp(_castStartTime + _duration - Time.realtimeSinceStartup, 0f, float.MaxValue) : 0f;

    private void OnEnable()
    {
        _castFinishTime = Time.realtimeSinceStartup - Cooldown;
    }

    private void OnGUI()
    {
        if (IsChanneling)
            _spriteRenderer.transform.localScale = Vector3.one * _radius;

        _spriteRenderer.enabled = IsChanneling;
    }

    public override void Execute(BaseUnit player)
    {
        if (!CanBeCasted)
            return;

        if (_useAbilityCoroutine != null)
            StopCoroutine(_useAbilityCoroutine);

        _useAbilityCoroutine = StartCoroutine(UseAbility(player));
    }

    private IEnumerator UseAbility(BaseUnit player)
    {
        _castStartTime = Time.realtimeSinceStartup;
        IsChanneling = true;

        while (RemainingChannelTime > 0)
        {
            BaseUnit closestEnemy = Physics2D.OverlapCircleAll(player.gameObject.transform.position, _radius)
                .Select(collider => { collider.TryGetComponent(out BaseUnit unit); return unit; })
                .Where(unit => unit != null && unit != player)
                .OrderBy(unit => Vector2.Distance(unit.gameObject.transform.position, player.gameObject.transform.position))
                .FirstOrDefault();

            if (closestEnemy != null)
                StealHealth(player, closestEnemy, _damage * Time.deltaTime);

            ValueChanged?.Invoke();

            yield return null;
        }

        IsChanneling = false;
        _castFinishTime = Time.realtimeSinceStartup;

        _cooldownAbilityCoroutine = StartCoroutine(CooldownAbility(player));
    }

    private IEnumerator CooldownAbility(BaseUnit player)
    {
        IsCooldowning = true;

        while (RemainingCooldown > 0)
        {
            ValueChanged?.Invoke();
            yield return null;
        }

        IsCooldowning = false;
        ValueChanged?.Invoke();
    }

    private void StealHealth(BaseUnit player, BaseUnit enemy, float damage)
    {
        float totalDamage = 0;
        float startHP;
        float finishHP;

        startHP = enemy.Health.Value;
        enemy.Health.Decrease(damage);
        finishHP = enemy.Health.Value;

        totalDamage += startHP - finishHP;

        player.Health.Increase(_vampirismPercent / 100 * totalDamage);
    }
}