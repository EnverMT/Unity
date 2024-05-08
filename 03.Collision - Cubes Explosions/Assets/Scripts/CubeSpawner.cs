using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    private float _multipleScaleOnEachSplit = 0.5f;
    private float _multipleChanceOnEachSplit = 0.5f;

    private int _splitCubeMin = 2;
    private int _splitCubeMax = 6;

    private float _explodeForce = 600;
    private float _explodeRange = 60;

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
        cubeObject.transform.localScale = scale;

        Cube cube = cubeObject.GetComponent<Cube>();
        if (null == cube)
            throw new UnityException("Game prefab must have Cube component");

        cube.SetRandomColor();
        cube.SplitChance = splitChance;

        cube.OnCubeSplit += SplitCube;

        return cubeObject;
    }

    private void SplitCube(GameObject cubeObject)
    {
        float splitChance = cubeObject.GetComponent<Cube>().SplitChance * _multipleChanceOnEachSplit;
        Vector3 scale = cubeObject.transform.localScale * _multipleScaleOnEachSplit;
        int newCubeCount = Random.Range(_splitCubeMin, _splitCubeMax);

        List<GameObject> childObjects = new();

        for (int i = 0; i < newCubeCount; i++)
        {
            Vector3 spawnPos = cubeObject.transform.position + GetRandomOffset(1f);
            childObjects.Add(Spawn(cubeObject, spawnPos, scale, splitChance));
        }

        RadialExplodeForce(cubeObject, childObjects);
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
