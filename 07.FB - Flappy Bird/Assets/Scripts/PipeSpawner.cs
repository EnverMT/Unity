using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

namespace FlappyBird
{
    public class PipeSpawner : MonoBehaviour
    {
        [SerializeField] private Pipe _pipePrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _releasePoint;
        [SerializeField] private float _delay;
        [SerializeField] private float _windowLength;
        [SerializeField] private float _windowRandomY;

        private ObjectPool<Pipe> _pipePool;
        private Queue<Pipe> _pipes;
        private Coroutine _spawnRoutine;

        private void OnValidate()
        {
            Assert.IsNotNull(_pipePrefab, nameof(_pipePrefab));
            Assert.IsNotNull(_spawnPoint, nameof(_spawnPoint));
            Assert.IsNotNull(_releasePoint, nameof(_releasePoint));
            Assert.IsTrue(_delay > 0, nameof(_delay));
            Assert.IsTrue(_windowLength > 0, nameof(_windowLength));
            Assert.IsTrue(_windowRandomY >= 0, nameof(_windowRandomY));
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
            if (_pipes.Count > 0 && IsPipeOutOfViewport(_pipes.Peek()))
                ReleasePipe();
        }

        private IEnumerator<WaitForSeconds> SpawnPeriodically()
        {
            WaitForSeconds wait = new WaitForSeconds(_delay);

            while (enabled)
            {
                SpawnPipe();
                yield return wait;
            }
        }

        private void SpawnPipe()
        {
            Pipe pipeTop = _pipePool.Get();
            Pipe pipeBot = _pipePool.Get();

            _pipes.Enqueue(pipeTop);
            _pipes.Enqueue(pipeBot);

            pipeTop.transform.SetParent(gameObject.transform, false);
            pipeBot.transform.SetParent(gameObject.transform, false);

            float randomY = Random.Range(-_windowRandomY, _windowRandomY);

            float pipeHeight = _pipePrefab.GetComponent<Renderer>().bounds.size.y;

            pipeBot.transform.position = new Vector3(_spawnPoint.position.x, _spawnPoint.position.y + randomY - (pipeHeight / 2 + _windowLength / 2), _spawnPoint.position.z);
            pipeTop.transform.position = new Vector3(_spawnPoint.position.x, _spawnPoint.position.y + randomY + (pipeHeight / 2 + _windowLength / 2), _spawnPoint.position.z);
        }

        private void ReleasePipe()
        {
            Pipe pipe = _pipes.Dequeue();
            _pipePool.Release(pipe);
        }

        private bool IsPipeOutOfViewport(Pipe pipe)
        {
            return pipe != null && pipe.transform.position.x < _releasePoint.position.x; // TO DO: to be dependent on camera width
        }
    }

}