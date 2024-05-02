using UnityEngine;

public class Cube2 : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 10f, 0, Space.Self);
    }
}
