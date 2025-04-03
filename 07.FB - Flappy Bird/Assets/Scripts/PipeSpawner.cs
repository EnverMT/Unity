using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private Pipe _pipePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _releasePoint;
    [SerializeField] private float _delay;

    private ObjectPool<Pipe> _pipePool;
    private Coroutine _spawnRoutine;

    private Queue<Pipe> _pipes;

    private void OnValidate()
    {
        Assert.IsNotNull(_pipePrefab, nameof(_pipePrefab));
        Assert.IsNotNull(_spawnPoint, nameof(_spawnPoint));
        Assert.IsNotNull(_releasePoint, nameof(_releasePoint));
        Assert.IsTrue(_delay > 0, nameof(_delay));
    }

    private void Awake()
    {
        _pipePool = new ObjectPool<Pipe>(
            createFunc: () => Instantiate(_pipePrefab),
            actionOnGet: pipe => pipe.gameObject.SetActive(true),
            actionOnRelease: pipe => pipe.gameObject.SetActive(false),
            actionOnDestroy: pipe => Destroy(pipe.gameObject));

        _pipes = new Queue<Pipe>();
    }

    private void OnEnable()
    {
        _spawnRoutine = StartCoroutine(SpawnPeriodically());
    }

    private void OnDisable()
    {
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine);

        _spawnRoutine = null;
    }

    private void Update()
    {
        if (_pipes.Count > 0)
        {
            Pipe pipe = _pipes.Peek();

            if (pipe != null && pipe.transform.position.x < _releasePoint.position.x)
            {
                _pipes.Dequeue();
                _pipePool.Release(pipe);
            }
        }
    }

    private IEnumerator<WaitForSeconds> SpawnPeriodically()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Pipe pipe = _pipePool.Get();

            _pipes.Enqueue(pipe);
            pipe.transform.SetParent(gameObject.transform, false);
            pipe.transform.position = _spawnPoint.position;

            yield return wait;
        }
    }
}
