using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Vector3 _cubeSpawnCenter;
    [SerializeField, Range(0f, 9f)] private float _spawnRadius = 9f;

    private ObjectPool<Cube> _cubePool;

    private const float MinCubeLifetime = 2f;
    private const float MaxCubeLifetime = 5f;

    private Coroutine _coroutine;

    #region UnityMethods
    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(CreateCube, OnCubeGet, OnCubeRelease, OnCubeDestroy);
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(SpawnPeriodically());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = null;
    }
    #endregion

    #region CubePool 
    private Cube CreateCube()
    {
        Cube cube = Instantiate(_cube);
        cube.SetPool(_cubePool);
        return cube;
    }

    private void OnCubeGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
    }

    private void OnCubeRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void OnCubeDestroy(Cube cube)
    {
        Destroy(cube.gameObject);
    }
    #endregion

    private IEnumerator SpawnPeriodically(float delay = 0.5f)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            SpawnCube();
            yield return wait;
        }
    }

    private Cube SpawnCube()
    {
        Cube cube = _cubePool.Get();

        cube.transform.SetParent(gameObject.transform, false);
        cube.transform.position = GetCubeSpawnPosition();
        cube.transform.rotation = Random.rotation;

        return cube;
    }

    private Vector3 GetCubeSpawnPosition()
    {
        Vector2 random2 = Random.insideUnitCircle * _spawnRadius;
        Vector3 random3 = new Vector3(random2.x, 0, random2.y);

        return random3 + _cubeSpawnCenter;
    }
}
