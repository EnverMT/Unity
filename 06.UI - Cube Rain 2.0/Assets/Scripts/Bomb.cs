using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class Bomb : BaseFieldObject
{
    [SerializeField, Range(0f, 10f)] private float _minTime = 2f;
    [SerializeField, Range(0f, 10f)] private float _maxTime = 5f;

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private float _dieTime;
    private float _delay;


    private void OnValidate()
    {
        Assert.IsTrue(_minTime <= _maxTime);
    }

    private void Update()
    {
        if (_dieTime > Time.realtimeSinceStartup && _delay >= _minTime)
        {
            Color color = _renderer.material.color;
            color.a = (_dieTime - Time.realtimeSinceStartup) / _delay;
            _renderer.material.color = color;
        }
    }

    private void OnEnable()
    {
        _delay = Random.Range(_minTime, _maxTime);
        _dieTime = _delay + Time.realtimeSinceStartup;

        StartCoroutine(DelayedExplode(_delay));
    }

    private IEnumerator DelayedExplode(float delay)
    {
        yield return new WaitForSeconds(delay);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Died?.Invoke(this);
        Destroy(gameObject);
    }
}