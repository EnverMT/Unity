using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private CoinSpawnPoints _coinSpawnPoints;
    [SerializeField] private int _healAmount = 100;

    private SpawnPoint[] _spawnPoints;
    private List<Coin> _coins = new();

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
            coin.Init(_healAmount);
            coin.transform.position = spawnPoint.transform.position;
        }
    }
}
