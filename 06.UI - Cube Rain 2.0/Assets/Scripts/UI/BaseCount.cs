using TMPro;
using UnityEngine;
using UnityEngine.Assertions;


public class BaseCount : MonoBehaviour
{
    [SerializeField] protected CubeSpawner CubeSpawner;
    [SerializeField] protected BombSpawner BombSpawner;

    [SerializeField] private TextMeshProUGUI _currentCount;
    [SerializeField] private TextMeshProUGUI _totalCount;

    private int _activeCubeCount;
    private int _activeBombCount;

    private int _totalCreatedCubeCount;
    private int _totalCreatedBombCount;

    private void OnValidate()
    {
        Assert.IsNotNull(CubeSpawner);
        Assert.IsNotNull(BombSpawner);
        Assert.IsNotNull(_currentCount);
        Assert.IsNotNull(_totalCount);
    }

    private void OnEnable()
    {
        CubeSpawner.Spawned += OnCubeSpawned;
        BombSpawner.Spawned += OnBombSpawned;
    }

    private void OnDisable()
    {
        CubeSpawner.Spawned -= OnCubeSpawned;
        BombSpawner.Spawned -= OnBombSpawned;
    }

    private void OnCubeSpawned(Cube cube)
    {
        cube.Died += OnCubeDied;
    }

    private void OnCubeDied(BaseFieldObject cube)
    {
        cube.Died -= OnCubeDied;
    }


    private void OnBombSpawned(Bomb bomb)
    {
        bomb.Died += OnBombDied;
    }

    private void OnBombDied(BaseFieldObject bomb)
    {
        bomb.Died -= OnBombDied;
    }
}
