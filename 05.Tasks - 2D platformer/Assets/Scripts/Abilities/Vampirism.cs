using System;
using System.Collections;
using System.Linq;
using UnityEngine;


public class Vampirism : BaseAbility, IBarIndicator
{
    [Header("Unit")]
    [SerializeField] private BaseUnit _unit;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("AbilityData")]
    [SerializeField] private float _damage;
    [SerializeField] private float _vampirismPercent;
    [SerializeField] private float _radius;
    [SerializeField] private float _castDuration;
    [SerializeField] private float _cooldownDuration;

    private float _castFinishTime;
    private float _cooldownFinishTime;

    private Coroutine _useAbilityCoroutine;
    private Coroutine _cooldownAbilityCoroutine;

    public event Action<float> IndicatorValueChanged;

    public override KeyCode ActivateKey => KeyCode.E;
    private bool _canBeCasted => !_isCasting && !_isCooldowning;
    private bool _isCasting => _castFinishTime > Time.realtimeSinceStartup;
    private bool _isCooldowning => _cooldownFinishTime > Time.realtimeSinceStartup;

    private float IndicatorValue
    {
        get
        {
            if (_isCasting)
                return (_castFinishTime - Time.realtimeSinceStartup) / _castDuration;

            if (_isCooldowning)
                return 1 - (_cooldownFinishTime - Time.realtimeSinceStartup) / _cooldownDuration;

            return _canBeCasted ? 1f : 0f;
        }
    }

    public float InitIndicatorValue => 1f;

    private void OnEnable()
    {
        _castFinishTime = Time.realtimeSinceStartup;
        _cooldownFinishTime = Time.realtimeSinceStartup;
    }

    private void OnGUI()
    {
        if (_isCasting)
            _spriteRenderer.transform.localScale = Vector3.one * _radius;

        _spriteRenderer.enabled = _isCasting;
    }

    public override void Execute(BaseUnit player)
    {
        if (!_canBeCasted)
            return;

        if (_useAbilityCoroutine != null)
            StopCoroutine(_useAbilityCoroutine);

        _useAbilityCoroutine = StartCoroutine(UseAbility(player));
    }

    private IEnumerator UseAbility(BaseUnit player)
    {
        _castFinishTime = Time.realtimeSinceStartup + _castDuration;

        while (_isCasting)
        {
            BaseUnit closestEnemy = GetClosestUnit(player);

            if (closestEnemy != null)
                StealHealth(player, closestEnemy, _damage * Time.deltaTime);

            IndicatorValueChanged?.Invoke(IndicatorValue);

            yield return null;
        }

        _cooldownAbilityCoroutine = StartCoroutine(CooldownAbility(player));
    }

    private IEnumerator CooldownAbility(BaseUnit player)
    {
        _cooldownFinishTime = Time.realtimeSinceStartup + _cooldownDuration;

        while (_isCooldowning)
        {
            IndicatorValueChanged?.Invoke(IndicatorValue);
            yield return null;
        }

        IndicatorValueChanged?.Invoke(IndicatorValue);
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

    private BaseUnit GetClosestUnit(BaseUnit player)
    {
        return Physics2D.OverlapCircleAll(player.gameObject.transform.position, _radius)
                .Select(collider => { collider.TryGetComponent(out BaseUnit unit); return unit; })
                .Where(unit => unit != null && unit != player)
                .OrderBy(unit => Vector2.Distance(unit.gameObject.transform.position, player.gameObject.transform.position))
                .FirstOrDefault();
    }
}