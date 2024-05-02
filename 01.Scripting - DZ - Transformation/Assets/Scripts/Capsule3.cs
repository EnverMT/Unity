using UnityEngine;

public class Capsule3 : MonoBehaviour
{
    [SerializeField] private Vector3 _scaleGrowthSpeed = new(0.02f, 0.02f, 0.02f);

    private void Update()
    {
        if (transform.localScale.magnitude > 2 || transform.localScale.magnitude < 1)
        {
            _scaleGrowthSpeed = -_scaleGrowthSpeed;
        }

        transform.localScale = transform.localScale + _scaleGrowthSpeed;
    }
}
