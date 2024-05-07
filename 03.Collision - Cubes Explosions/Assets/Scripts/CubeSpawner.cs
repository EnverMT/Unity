using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 35; i++)
        {
            Vector3 size = new(1, 1, 1);
            Vector3 position = new(Random.Range(-9, 9), Random.Range(2, 9), Random.Range(-9, 9));
            Quaternion rotarion = Quaternion.Euler(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
            Spawn(PrimitiveType.Cube, size, position, rotarion);
        }
    }

    private void Spawn(PrimitiveType type, Vector3 size, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = GameObject.CreatePrimitive(type);
        gameObject.transform.position = position;
        gameObject.transform.localScale = size;
        gameObject.transform.rotation = rotation;
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<Cube>();
    }
}
