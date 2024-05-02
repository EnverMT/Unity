using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 _speed;
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime);
    }
}
