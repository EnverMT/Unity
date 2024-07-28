using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 0.5f;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    private Coroutine _spawnCoroutine;


    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnPeriodically(_spawnDelay));
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnPeriodically(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Cube cube = _cubeSpawner.Spawn(_cubeSpawner.gameObject.transform.position);
            cube.Died += OnDied;

            yield return wait;
        }
    }

    private void OnDied(BaseFieldObject cube)
    {
        cube.Died -= OnDied;

        _bombSpawner.Spawn(cube.transform.position);
    }
}