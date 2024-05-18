using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Cube cube;
    [Header("Terrain")]
    [SerializeField] private Terrain terrain;

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
        GenerateCubesOnTerrain(cube, _initialCubeCount);
    }

    public Cube[] Split(Cube cube)
    {
        float splitChance = cube.SplitChance * _multipleChanceOnEachSplit;
        float scale = cube.ScaleMultiplier * _multipleScaleOnEachSplit;
        int newCubeCount = Random.Range(_splitCubeMin, _splitCubeMax);

        List<Cube> cubes = new List<Cube>();

        for (int i = 0; i < newCubeCount; i++)
        {
            Vector3 spawnPos = cube.transform.position + GetRandomOffset();
            Cube spawnedCube = Spawn(cube, spawnPos, scale, splitChance);
            cubes.Add(spawnedCube);
        }

        return cubes.ToArray();
    }

    private void GenerateCubesOnTerrain(Cube cube, int count)
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;

        float xTerrainPos = terrain.transform.position.x;
        float yTerrainPos = terrain.transform.position.y;
        float zTerrainPos = terrain.transform.position.z;

        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(xTerrainPos, xTerrainPos + terrainWidth);
            float randomY = Random.Range(yTerrainPos + _minHeight, yTerrainPos + _maxHeight);
            float randomZ = Random.Range(zTerrainPos, zTerrainPos + terrainLength);

            Spawn(cube, new Vector3(randomX, randomY, randomZ), _initialScale);
        }
    }

    private Vector3 GetRandomOffset(float radius = 1f)
    {
        float offsetX = Random.Range(-1f, 1f) * radius;
        float offsetY = Random.Range(0f, 1f) * radius;
        float offsetZ = Random.Range(-1f, 1f) * radius;

        return new Vector3(offsetX, offsetY, offsetZ);
    }

    private Cube Spawn(Cube cubePrefab, Vector3 position, float scale, float splitChance = 1f)
    {
        Cube cube = Instantiate(cubePrefab, position, Quaternion.identity);
        cube.transform.SetParent(gameObject.transform, false);
        cube.Init(splitChance, scale);

        return cube;
    }
}
