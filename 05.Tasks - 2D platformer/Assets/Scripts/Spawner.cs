using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private CoinSpawnPoints _coinSpawnPoints;

    private SpawnPoint[] _spawnPoints;
    private List<Coin> _coins;

    private void OnValidate()
    {
        Assert.IsNotNull(_coin);
        Assert.IsNotNull(_coinSpawnPoints);
    }

    private void Awake()
    {
        _spawnPoints = _coinSpawnPoints.GetComponentsInChildren<SpawnPoint>();
    }

    private void OnEnable()
    {
        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            Coin coin = Instantiate(_coin, spawnPoint.gameObject.transform);
            _coins.Add(coin);
            coin.transform.position = spawnPoint.transform.position; 
        }
    }

    private void OnDisable()
    {
        foreach (Coin coin in _coins)
        {
            Destroy(coin.gameObject);
        }
    }
}
