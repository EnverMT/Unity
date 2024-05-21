using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    private const float SpawnRate = 2f;

    [SerializeField] private SpawnPoint[] _spawnPoints;    
        
    private Coroutine _spawnCoroutine;

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

    private IEnumerator SpawnPeriodically(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            _spawnPoints[Random.Range(0, _spawnPoints.Length)].Spawn();            
            yield return wait;
        }
    }    
}
