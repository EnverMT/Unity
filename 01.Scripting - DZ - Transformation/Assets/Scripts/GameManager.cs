using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        GameObject sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere1.transform.localPosition = new Vector3(0, 0, 0);
        sphere1.AddComponent<Sphere1>();

        GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube2.transform.localPosition = new Vector3(2, 0, 0);
        cube2.AddComponent<Cube2>();

        GameObject capsule3 = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule3.transform.localPosition = new Vector3(4, 0, 0);
        capsule3.AddComponent<Capsule3>();

        GameObject cube4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube4.transform.localPosition = new Vector3(6, 0, 0);
        cube4.AddComponent<Cube4>();
    }
}
