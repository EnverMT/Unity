using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private KeyCode _left;
    [SerializeField] private KeyCode _right;

    private void Update()
    {
        if (Input.GetKey(_right))
            transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if (Input.GetKey(_left))
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}
