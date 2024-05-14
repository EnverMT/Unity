using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Terrain _terrain;

    private const float _multipleScaleOnEachSplit = 0.5f;
    private const float _multipleChanceOnEachSplit = 0.5f;

    private const int _splitCubeMin = 2;
    private const int _splitCubeMax = 6;

    private const int _initialCubeCount = 8;
    private const float _initialScale = 1f;

    private const float _minHeight = 5f;
    private const float _maxHeight = 10f;

    private void Start()
    {
        GenerateCubesOnTerrain(_cube, _initialCubeCount);
    }

    private void GenerateCubesOnTerrain(Cube cube, int count)
    {
        float terrainWidth = _terrain.terrainData.size.x;
        float terrainLength = _terrain.terrainData.size.z;

        float xTerrainPos = _terrain.transform.position.x;
        float yTerrainPos = _terrain.transform.position.y;
        float zTerrainPos = _terrain.transform.position.z;

        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(xTerrainPos, xTerrainPos + terrainWidth);
            float randomY = Random.Range(yTerrainPos + _minHeight, yTerrainPos + _maxHeight);
            float randomZ = Random.Range(zTerrainPos, zTerrainPos + terrainLength);            

            Spawn(cube, new(randomX, randomY, randomZ), _initialScale);
        }
    }

    private GameObject Spawn(Cube cubePrefab, Vector3 position, float scale, float splitChance = 1f)
    {
        GameObject cubeObject = Instantiate(cubePrefab.gameObject, position, Quaternion.identity);
        cubeObject.transform.SetParent(gameObject.transform, false);

        Cube cube = cubeObject.GetComponent<Cube>();

        cube.Init(splitChance, scale);
        cube.OnSplitting += Splitting;

        return cubeObject;
    }

    private Collider[] Splitting(Cube cube)
    {
        cube.OnSplitting -= Splitting;

        float splitChance = cube.SplitChance * _multipleChanceOnEachSplit;
        float scale = cube.ScaleMultiplier * _multipleScaleOnEachSplit;
        int newCubeCount = Random.Range(_splitCubeMin, _splitCubeMax);

        List<Collider> childObjects = new();

        for (int i = 0; i < newCubeCount; i++)
        {
            Vector3 spawnPos = cube.transform.position + GetRandomOffset(1f);
            GameObject spawnedCube = Spawn(cube, spawnPos, scale, splitChance);
            if (spawnedCube.TryGetComponent(out Collider collider))
            {
                childObjects.Add(collider);
            }
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
