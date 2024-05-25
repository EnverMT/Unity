using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private KeyCode _leftKey;
    [SerializeField] private KeyCode _rightKey;
    [SerializeField] private float _speed;

    [Header("Jump")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private bool _isGrounded;

    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        _isGrounded = true;
    }

    private void Update()
    {
        if (_isGrounded && Input.GetKey(_jumpKey))
        {
            _body.velocity = Vector3.up * _jumpSpeed;
            _isGrounded = false;
        }

        if (Input.GetKey(_rightKey))
            transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if (Input.GetKey(_leftKey))
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}
