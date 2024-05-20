using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    private const float SpawnRate = 2f;

    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private Target _target;

    private ObjectPool<Enemy> _enemyPool;
    private Coroutine _spawnCoroutine;

    #region UnityMethods
    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(OnEnemyCreate, OnEnemyGet, OnEnemyRelease, OnEnemyDestroy);
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnPeriodically(SpawnRate));
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        _spawnCoroutine = null;
    }
    #endregion

    #region EnemyObjectPoolMethods 
    private Enemy OnEnemyCreate()
    {
        Enemy enemy = Instantiate(_enemy);
        return enemy;
    }

    private void OnEnemyGet(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnEnemyRelease(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnEnemyDestroy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
    #endregion

    private IEnumerator SpawnPeriodically(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private Enemy Spawn()
    {
        Enemy enemy = _enemyPool.Get();                
        
        enemy.transform.SetParent(gameObject.transform, false);
        enemy.Died += OnEnemyDied;

        return enemy;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;

        _enemyPool.Release(enemy);
    }
}
