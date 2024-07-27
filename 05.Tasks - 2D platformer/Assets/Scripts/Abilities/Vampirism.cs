using Platformer.Base;
using Platformer.UI.Indicator;

using System;
using System.Collections;
using System.Linq;

using UnityEngine;

namespace Platformer.Ability
{
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

        public bool CanBeCasted => !IsCasting && !IsCooldowning;
        public bool IsCasting => _castFinishTime > Time.realtimeSinceStartup;
        public bool IsCooldowning => _cooldownFinishTime > Time.realtimeSinceStartup;
        public float IndicatorValue
        {
            get
            {
                if (IsCasting)
                    return (_castFinishTime - Time.realtimeSinceStartup) / _castDuration;

                if (IsCooldowning)
                    return 1 - (_cooldownFinishTime - Time.realtimeSinceStartup) / _cooldownDuration;

                return CanBeCasted ? 1f : 0f;
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
            if (IsCasting)
                _spriteRenderer.transform.localScale = Vector3.one * _radius;

            _spriteRenderer.enabled = IsCasting;
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
            _castFinishTime = Time.realtimeSinceStartup + _castDuration;

            while (IsCasting)
            {
                BaseUnit closestEnemy = GetClosestUnit(player, _radius);

                if (closestEnemy != null)
                    StealHealth(player, closestEnemy, _damage * Time.deltaTime);

                IndicatorValueChanged?.Invoke(IndicatorValue);

                yield return null;
            }

            if (_cooldownAbilityCoroutine != null)
                StopCoroutine(_cooldownAbilityCoroutine);

            _cooldownAbilityCoroutine = StartCoroutine(CooldownAbility());
        }

        private IEnumerator CooldownAbility()
        {
            _cooldownFinishTime = Time.realtimeSinceStartup + _cooldownDuration;

            while (IsCooldowning)
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

        private BaseUnit GetClosestUnit(BaseUnit player, float radius)
        {
            return Physics2D.OverlapCircleAll(player.gameObject.transform.position, radius)
                    .Select(collider => { collider.TryGetComponent(out BaseUnit unit); return unit; })
                    .Where(unit => unit != null && unit != player && player.IsEnemy(unit))
                    .OrderBy(unit => Vector2.Distance(unit.gameObject.transform.position, player.gameObject.transform.position))
                    .FirstOrDefault();
        }
    }
}