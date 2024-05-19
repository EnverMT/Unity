using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private ObjectPool<Enemy> _enemyPool;

    private Coroutine _spawnCoroutine;

    private void Awake()
    {
    }

    #region EnemyObjectPoolMethods 
    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemy);
        enemy.SetPool(_enemyPool);
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

    private IEnumerator SpawnPeriodically(float delay = 0.5f)
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
        return enemy;
    }
}
