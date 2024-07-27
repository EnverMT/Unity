using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class GameController : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private Coroutine _spawnCoroutine;

    private void OnValidate()
    {
        Assert.IsNotNull(_spawner);
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnPeriodically<Cube>());
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnPeriodically<T>(float delay = 0.5f) where T : BaseFieldObject
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            BaseFieldObject obj = _spawner.Spawn<T>(null);
            obj.Died += OnDied;

            yield return wait;
        }
    }

    private void OnDied(BaseFieldObject obj)
    {
        obj.Died -= OnDied;

        _spawner.Spawn<Bomb>(obj.transform.position);
    }
}