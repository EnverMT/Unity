using System.Collections;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Vampirism : BaseAbility
{
    [SerializeField] private float _damage;
    [SerializeField] private float _tickRate;
    [SerializeField] private float _vampirismPercent;
    [SerializeField] private float _radius;
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldown;

    [SerializeField] private BaseUnit _unit;

    private float _lastCastStartTime;
    private float _lastCastFinishTime;

    private Coroutine _useAbilityCoroutine;
    private bool _isChanneling = false;

    private SpriteRenderer _spriteRenderer;

    public override KeyCode ActivateKey => KeyCode.E;
    public override bool CanBeCasted => RemainingCooldown == 0f && IsChanneling == false;
    public override bool IsChanneling => _isChanneling;
    public override float RemainingChannelTime => IsChanneling ? Mathf.Clamp(_lastCastStartTime + _duration - Time.realtimeSinceStartup, 0f, float.MaxValue) : 0f;
    public override float RemainingCooldown => Mathf.Clamp(_lastCastFinishTime + _cooldown - Time.realtimeSinceStartup, 0f, float.MaxValue);


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(ActivateKey))
            Execute(_unit);
    }

    private void OnGUI()
    {
        if (IsChanneling)
        {
            _spriteRenderer.transform.localScale = Vector3.one * _radius;
        }
        else
        {
            _spriteRenderer.transform.localEulerAngles = Vector3.zero;
        }

    }

    public override void Execute(BaseUnit player)
    {
        if (!CanBeCasted)
            return;

        Debug.Log($"Vampirism execute");

        if (_useAbilityCoroutine != null)
            player.StopCoroutine(_useAbilityCoroutine);

        _spriteRenderer.transform.localScale = Vector3.one * _radius;
        _useAbilityCoroutine = player.StartCoroutine(UseAbility(player));
    }

    private IEnumerator UseAbility(BaseUnit player)
    {
        WaitForSeconds wait = new WaitForSeconds(_tickRate);
        _lastCastStartTime = Time.realtimeSinceStartup;
        _isChanneling = true;

        while (RemainingChannelTime > 0)
        {
            BaseUnit[] enemies = Physics2D.OverlapCircleAll(player.gameObject.transform.position, _radius)
                .Select(collider => { collider.TryGetComponent(out BaseUnit unit); return unit; })
                .Where(unit => unit != null)
                .ToArray();

            if (enemies.Length == 0)
                yield return wait;

            StealHealth(player, enemies);

            yield return wait;
        }

        _isChanneling = false;
        _lastCastFinishTime = Time.realtimeSinceStartup;
    }

    private void StealHealth(BaseUnit player, BaseUnit[] enemies)
    {
        float totalDamage = 0;
        float startHP, finishHP;

        foreach (BaseUnit unit in enemies)
        {
            startHP = unit.Health.Value;
            unit.Health.ChangeValue(-_damage);
            finishHP = unit.Health.Value;

            totalDamage += startHP - finishHP;
        }

        player.Health.ChangeValue(_vampirismPercent * totalDamage);
    }
}