using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private const float _multipleScaleOnEachSplit = 0.5f;
    private const float _multipleChanceOnEachSplit = 0.5f;

    private const int _splitCubeMin = 2;
    private const int _splitCubeMax = 6;    

    private const int _initialCubeCount = 8;

    private void Start()
    {
        InitializeObjectsOnScene(_cubePrefab.gameObject, _initialCubeCount);
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
        cubeObject.transform.SetParent(gameObject.transform, false);

        Cube cube = cubeObject.GetComponent<Cube>();
        if (cube == null)
            throw new UnityException("Prefab must have Cube script");

        cube.Init(splitChance, scale);
        cube.OnSplitting += Splitting;

        return cubeObject;
    }

    private GameObject[] Splitting(Cube cube)
    {
        cube.OnSplitting -= Splitting;

        float splitChance = cube.SplitChance * _multipleChanceOnEachSplit;
        Vector3 scale = cube.gameObject.transform.localScale * _multipleScaleOnEachSplit;
        int newCubeCount = Random.Range(_splitCubeMin, _splitCubeMax);

        List<GameObject> childObjects = new();

        for (int i = 0; i < newCubeCount; i++)
        {
            Vector3 spawnPos = cube.transform.position + GetRandomOffset(1f);
            childObjects.Add(Spawn(cube.gameObject, spawnPos, scale, splitChance));
        }

        return childObjects.ToArray();
    }

    private Vector3 GetRandomOffset(float radius = 1f)
    {
        float offsetX = Random.Range(-1f, 1f) * radius;
        float offsetY = Random.Range(0f, 1f) * radius;
        float offsetZ = Random.Range(-1f, 1f) * radius;

        return new Vector3(offsetX, offsetY, offsetZ);
    }
}
