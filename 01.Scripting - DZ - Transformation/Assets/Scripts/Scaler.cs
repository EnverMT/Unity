using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Vector3 _scaleGrowthSpeed;

    private void Update()
    {
        transform.localScale += _scaleGrowthSpeed * Time.deltaTime;
    }
}
