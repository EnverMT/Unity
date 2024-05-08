using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 size = new(1, 1, 1);
            Vector3 position = new(Random.Range(-9, 9), Random.Range(2, 9), Random.Range(-9, 9));
            SpawnCube(PrimitiveType.Cube, size, position);
        }
    }

    public GameObject SpawnCube(PrimitiveType type, Vector3 scale, Vector3 position)
    {
        GameObject cube = GameObject.CreatePrimitive(type);

        cube.transform.position = position;
        cube.transform.localScale = scale;

        cube.AddComponent<Rigidbody>();

        cube.AddComponent<Cube>().SetSpawner(this);

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
        GameObject explosionPrefab = Resources.Load<GameObject>("Prefabs/VFX_Explosion");

        GameObject effect = Instantiate(explosionPrefab, cube.transform.position, Quaternion.identity);

        Destroy(cube);
        Destroy(effect, 1);
    }
}
