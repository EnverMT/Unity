using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    private const float _multipleScaleOnEachSplit = 0.5f;
    private const float _multipleChanceOnEachSplit = 0.5f;

    private const float _explodeForce = 600;
    private const float _explodeRange = 60;

    private const int _splitCubeMin = 2;
    private const int _splitCubeMax = 6;

    private void OnValidate()
    {
        if (_cubePrefab.GetComponent<Cube>() == null)
            _cubePrefab.AddComponent<Cube>();
    }

    private void Start()
    {
        InitializeObjectsOnScene(_cubePrefab, 5);
    }

    private void InitializeObjectsOnScene(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new(Random.Range(-7, 7), Random.Range(2, 6), Random.Range(-7, 7));
            Spawn(prefab, position, prefab.transform.localScale);
        }
    }

    private GameObject Spawn(GameObject prefab, Vector3 position, Vector3 scale, float splitChance = 1f)
    {
        GameObject cubeObject = Instantiate(prefab, position, prefab.transform.rotation);

        Cube cube = cubeObject.GetComponent<Cube>();

        cube.Init(splitChance, scale);
        cube.OnSplitting += Cube_OnSplitting;

        return cubeObject;
    }

    private void Cube_OnSplitting(Cube cube)
    {
        float splitChance = cube.SplitChance * _multipleChanceOnEachSplit;
        Vector3 scale = cube.gameObject.transform.localScale * _multipleScaleOnEachSplit;
        int newCubeCount = Random.Range(_splitCubeMin, _splitCubeMax);

        List<GameObject> childObjects = new();

        for (int i = 0; i < newCubeCount; i++)
        {
            Vector3 spawnPos = cube.transform.position + GetRandomOffset(1f);
            childObjects.Add(Spawn(cube.gameObject, spawnPos, scale, splitChance));
        }

        RadialExplodeForce(cube.gameObject, childObjects);
    }

    private void RadialExplodeForce(GameObject cubeObject, List<GameObject> childObjects)
    {
        foreach (GameObject child in childObjects)
        {
            child.GetComponent<Rigidbody>().AddExplosionForce(_explodeForce, cubeObject.transform.position, _explodeRange);
        }
    }

    private Vector3 GetRandomOffset(float radius = 1f)
    {
        float offsetX = Random.Range(-1f, 1f) * radius;
        float offsetY = Random.Range(0f, 1f) * radius;
        float offsetZ = Random.Range(-1f, 1f) * radius;

        return new Vector3(offsetX, offsetY, offsetZ);
    }
}
