using UnityEngine;

[RequireComponent (typeof(CubeSpawner))]
public class Explosion : MonoBehaviour
{
    private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner = GetComponent<CubeSpawner>();
        _cubeSpawner.OnCubeSpawned += OnCubeSpawned;
    }

    private void OnDisable()
    {
        _cubeSpawner.OnCubeSpawned -= OnCubeSpawned;
    }

    private void OnCubeSpawned(GameObject arg0)
    {
        Debug.Log("Explosion");
    }    
}
