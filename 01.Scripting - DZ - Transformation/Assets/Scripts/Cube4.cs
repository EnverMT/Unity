using UnityEngine;

public class Cube4 : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 0.2f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private Vector3 _scaleGrowthSpeed = new(0.02f, 0.02f, 0.02f);

    private void Update()
    {        
        transform.Translate(new Vector3(_movementSpeed, 0, 0), Space.Self);

        transform.Rotate(0, _rotationSpeed, 0, Space.Self);
        
        
        if (transform.localScale.magnitude > 2 || transform.localScale.magnitude < 1)
        {
            _scaleGrowthSpeed = -_scaleGrowthSpeed;
        }

        transform.localScale = transform.localScale + _scaleGrowthSpeed;
    }
}
