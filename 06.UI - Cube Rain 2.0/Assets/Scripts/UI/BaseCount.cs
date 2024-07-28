using TMPro;
using UnityEngine;
using UnityEngine.Assertions;


public class BaseCount : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    [SerializeField] private TextMeshProUGUI _currentCount;
    [SerializeField] private TextMeshProUGUI _totalCount;

    private int _activeCubeCount = 0;
    private int _activeBombCount = 0;

    private int _totalCreatedCubeCount = 0;
    private int _totalCreatedBombCount = 0;

    private void OnValidate()
    {
        Assert.IsNotNull(_cubeSpawner);
        Assert.IsNotNull(_bombSpawner);
        Assert.IsNotNull(_currentCount);
        Assert.IsNotNull(_totalCount);
    }

    private void OnEnable()
    {
        _cubeSpawner.Spawned += OnCubeSpawned;
        _bombSpawner.Spawned += OnBombSpawned;
    }

    private void OnDisable()
    {
        _cubeSpawner.Spawned -= OnCubeSpawned;
        _bombSpawner.Spawned -= OnBombSpawned;
    }

    private void OnGUI()
    {
        _currentCount.text = $"Active cubes = {_activeCubeCount}\n";
        _currentCount.text += $"Active bombs = {_activeBombCount}\n";

        _totalCount.text = $"Total cubes = {_totalCreatedCubeCount}\n";
        _totalCount.text += $"Total bombs = {_totalCreatedBombCount}\n";
    }

    private void OnCubeSpawned(Cube cube)
    {
        cube.Died += OnCubeDied;

        _activeCubeCount++;
        _totalCreatedCubeCount++;
    }

    private void OnCubeDied(BaseFieldObject cube)
    {
        cube.Died -= OnCubeDied;

        _activeCubeCount--;
    }


    private void OnBombSpawned(Bomb bomb)
    {
        bomb.Died += OnBombDied;

        _totalCreatedBombCount++;
        _activeBombCount++;
    }

    private void OnBombDied(BaseFieldObject bomb)
    {
        bomb.Died -= OnBombDied;

        _activeBombCount--;
    }
}