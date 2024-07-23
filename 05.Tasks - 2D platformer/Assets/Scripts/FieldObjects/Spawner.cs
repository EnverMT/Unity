using UnityEngine;
using UnityEngine.Assertions;

namespace Platformer.FieldObjects
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnPoints _spawnPointsCollectionParent;

        private SpawnPoint[] _spawnPoints;

        private void OnValidate()
        {
            Assert.IsNotNull(_spawnPointsCollectionParent);
        }

        private void Awake()
        {
            _spawnPoints = _spawnPointsCollectionParent.GetComponentsInChildren<SpawnPoint>();
        }

        private void OnEnable()
        {
            foreach (SpawnPoint spawnPoint in _spawnPoints)
            {
                spawnPoint.Spawn();
            }
        }
    }
}