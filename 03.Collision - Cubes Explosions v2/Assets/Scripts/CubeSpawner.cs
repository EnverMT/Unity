using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{    
     private const float MultipleScaleOnEachSplit = 0.5f;
     private const float MultipleChanceOnEachSplit = 0.5f;

     private const int SplitCubeMin = 2;
     private const int SplitCubeMax = 6;

     private const int InitialCubeCount = 8;
     private const float InitialScale = 1f;
  
     private const float MinHeight = 5f;
     private const float MaxHeight = 10f;

    [Header("Prefabs")]
    [SerializeField] private Cube cube;
    [Header("Terrain")]
    [SerializeField] private Terrain terrain;

    private void Start()
    {
        this.GenerateCubesOnTerrain(this.cube, InitialCubeCount);
    }

    private void GenerateCubesOnTerrain(Cube cube, int count)
    {
        float terrainWidth = this.terrain.terrainData.size.x;
        float terrainLength = this.terrain.terrainData.size.z;

        float xTerrainPos = this.terrain.transform.position.x;
        float yTerrainPos = this.terrain.transform.position.y;
        float zTerrainPos = this.terrain.transform.position.z;

        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(xTerrainPos, xTerrainPos + terrainWidth);
            float randomY = Random.Range(yTerrainPos + MinHeight, yTerrainPos + MaxHeight);
            float randomZ = Random.Range(zTerrainPos, zTerrainPos + terrainLength);

            this.Spawn(cube, new Vector3(randomX, randomY, randomZ), InitialScale);
        }
    }

    private GameObject Spawn(Cube cubePrefab, Vector3 position, float scale, float splitChance = 1f)
    {
        GameObject cubeObject = Instantiate(cubePrefab.gameObject, position, Quaternion.identity);
        cubeObject.transform.SetParent(this.gameObject.transform, false);

        Cube cube = cubeObject.GetComponent<Cube>();

        cube.Init(splitChance, scale);
        cube.OnSplitting += this.Split;

        return cubeObject;
    }

    private Collider[] Split(Cube cube)
    {
        cube.OnSplitting -= this.Split;

        float splitChance = cube.SplitChance * MultipleChanceOnEachSplit;
        float scale = cube.ScaleMultiplier * MultipleScaleOnEachSplit;
        int newCubeCount = Random.Range(SplitCubeMin, SplitCubeMax);

        List<Collider> childObjects = new();

        for (int i = 0; i < newCubeCount; i++)
        {
            Vector3 spawnPos = cube.transform.position + this.GetRandomOffset(1f);
            GameObject spawnedCube = this.Spawn(cube, spawnPos, scale, splitChance);
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
