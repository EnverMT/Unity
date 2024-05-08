using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    private void Start()
    {
        InitializeCubesOnScene(5);
    }

    private void InitializeCubesOnScene(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 scale = new(1, 1, 1);
            Vector3 position = new(Random.Range(-7, 7), Random.Range(2, 6), Random.Range(-7, 7));
            SpawnCube(scale, position);
        }
    }

    public GameObject SpawnCube(Vector3 scale, Vector3 position)
    {
        GameObject cube = GameObject.Instantiate(_cube);
        cube.transform.position = position;
        cube.transform.localScale = scale;        

        var rand = new System.Random();
        Color randomColor = new Color((float)rand.NextDouble(),
                                      (float)rand.NextDouble(),
                                      (float)rand.NextDouble(),
                                      (float)rand.NextDouble());
        cube.GetComponent<Renderer>().material.SetColor("_Color", randomColor);

        return cube;
    }

    public void DestroyCube(GameObject cube)
    {
        //GameObject explosionPrefab = Resources.Load<GameObject>("Prefabs/VFX_Explosion");

        //GameObject effect = Instantiate(explosionPrefab, cube.transform.position, Quaternion.identity);

        Destroy(cube);
        //Destroy(effect, 1);
    }

    public GameObject SplitCube(GameObject cube)
    {
        float radius = 2f;
        Vector3 randomOffset = Random.insideUnitSphere * radius;
        if (randomOffset.y < cube.transform.position.y)
            randomOffset.y *= -1;           // To prevent dropping under terrain

        Vector3 spawnPos = cube.transform.position + randomOffset;
        GameObject newCube = this.SpawnCube(cube.transform.localScale, spawnPos);

        newCube.transform.localScale /= 2;        

        return newCube;
    }
}
