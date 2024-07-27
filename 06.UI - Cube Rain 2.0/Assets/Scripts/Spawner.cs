using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Vector3 _spawnCenter;
    [SerializeField, Range(0f, 9f)] private float _spawnRadius = 9f;

    private ObjectPool<BaseFieldObject> _cubePool;

    private Coroutine _spawnCoroutine;

    #region UnityMethods
    private void Awake()
    {
        _cubePool = new ObjectPool<BaseFieldObject>(Create, OnCubeGet, OnCubeRelease, OnCubeDestroy);
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnPeriodically());
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        _spawnCoroutine = null;
    }
    #endregion

    #region CubePoolMethods 
    private BaseFieldObject Create()
    {
        return Instantiate(_cube);
    }

    private void OnCubeGet(BaseFieldObject obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnCubeRelease(BaseFieldObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnCubeDestroy(BaseFieldObject obj)
    {
        Destroy(obj.gameObject);
    }
    #endregion

    private IEnumerator SpawnPeriodically(float delay = 0.5f)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Spawn<Cube>();
            yield return wait;
        }
    }

    private BaseFieldObject Spawn<T>() where T : BaseFieldObject
    {
        T obj = (T)_cubePool.Get();

        obj.transform.SetParent(gameObject.transform, false);
        obj.transform.position = GetSpawnPosition();
        obj.transform.rotation = Random.rotation;

        return obj;
    }

    private Vector3 GetSpawnPosition()
    {
        Vector2 random2 = Random.insideUnitCircle * _spawnRadius;
        Vector3 random3 = new Vector3(random2.x, 0, random2.y);

        return random3 + _spawnCenter;
    }
}
