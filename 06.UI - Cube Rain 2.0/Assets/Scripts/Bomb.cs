using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class Bomb : BaseFieldObject
{
    [SerializeField, Range(0f, 10f)] private float _minTime = 2f;
    [SerializeField, Range(0f, 10f)] private float _maxTime = 5f;

    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 10f;

    private Coroutine _coroutine;

    private void OnValidate()
    {
        Assert.IsTrue(_minTime <= _maxTime);
    }

    private void OnEnable()
    {
        float delay = Random.Range(_minTime, _maxTime);

        StartCoroutine(DelayedExplode(delay));
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

        Destroy(gameObject);
    }
}