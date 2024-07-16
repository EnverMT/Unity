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
    [SerializeField] private float _cooldown;

    private float _castStartTime;
    private float _castFinishTime;

    private Coroutine _useAbilityCoroutine;
    private Coroutine _cooldownAbilityCoroutine;
    private bool _isChanneling = false;
    private bool _isCooldowning = false;

    public override event Action ValueChanged;

    public override KeyCode ActivateKey => KeyCode.E;
    public override bool CanBeCasted => RemainingCooldown == 0f && IsChanneling == false;
    public override float Cooldown => _cooldown;
    public override bool IsCooldowning => _isCooldowning;
    public override float RemainingCooldown => Mathf.Clamp(_castFinishTime + _cooldown - Time.realtimeSinceStartup, 0f, float.MaxValue);

    public float ChannelTime => _duration;
    public bool IsChanneling => _isChanneling;
    public float RemainingChannelTime => IsChanneling ? Mathf.Clamp(_castStartTime + _duration - Time.realtimeSinceStartup, 0f, float.MaxValue) : 0f;


    private void OnEnable()
    {
        _castFinishTime = Time.realtimeSinceStartup - _cooldown;
    }

    private void OnGUI()
    {
        if (_isChanneling)
            _spriteRenderer.transform.localScale = Vector3.one * _radius;

        _spriteRenderer.enabled = IsChanneling;
    }

    private void Update()
    {
        if (Input.GetKeyDown(ActivateKey))
            Execute(_unit);
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
        _isChanneling = true;

        while (RemainingChannelTime > 0)
        {
            BaseUnit[] enemies = Physics2D.OverlapCircleAll(player.gameObject.transform.position, _radius)
                .Select(collider => { collider.TryGetComponent(out BaseUnit unit); return unit; })
                .Where(unit => unit != null && unit != player)
                .ToArray();

            if (enemies.Length == 0)
                yield return null;

            StealHealth(player, enemies, _damage * Time.deltaTime);
            ValueChanged?.Invoke();
            yield return null;
        }

        _isChanneling = false;
        _castFinishTime = Time.realtimeSinceStartup;

        _cooldownAbilityCoroutine = StartCoroutine(CooldownAbility(player));
    }

    private IEnumerator CooldownAbility(BaseUnit player)
    {
        _isCooldowning = true;

        while (RemainingCooldown > 0)
        {
            ValueChanged?.Invoke();
            yield return null;
        }

        _isCooldowning = false;
        ValueChanged?.Invoke();
    }

    private void StealHealth(BaseUnit player, BaseUnit[] enemies, float damage)
    {
        float totalDamage = 0;
        float startHP, finishHP;

        foreach (BaseUnit unit in enemies)
        {
            startHP = unit.Health.Value;
            unit.Health.ChangeValue(-damage);
            finishHP = unit.Health.Value;

            totalDamage += startHP - finishHP;
        }

        player.Health.ChangeValue(_vampirismPercent / 100 * totalDamage);
    }
}