using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : BaseFieldObject
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private bool _isCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided == false && collision.gameObject.TryGetComponent<Plane>(out _))
        {
            _isCollided = true;

            StartCoroutine(Die(Random.Range(_minLifeTime, _maxLifeTime)));

            Collided?.Invoke(this);
        }
    }


    private IEnumerator Die(float delay)
    {
        yield return new WaitForSeconds(delay);

        Died?.Invoke(this);
    }
}
