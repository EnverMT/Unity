using System.Collections;
using UnityEngine;

public class InstantiateBulletsShooting : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeWaitShooting = 1f;

    private void Start()
    {
        StartCoroutine(CreateBullet());
    }

    private IEnumerator CreateBullet()
    {
        WaitForSeconds delay = new WaitForSeconds(_timeWaitShooting);

        while (enabled)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Rigidbody bullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);

            bullet.transform.up = direction;
            bullet.velocity = direction * _speed;

            yield return delay;
        }
    }
}