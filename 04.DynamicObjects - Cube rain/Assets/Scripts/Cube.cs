using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private const float MinLifetime = 2f;
    private const float MaxLifetime = 5f;

    private Renderer _renderer;

    private ObjectPool<Cube> _pool;

    private bool _isColorChanged;

    #region UnityMethods
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    private void OnEnable()
    {
        SetColor(Color.gray);
        _isColorChanged = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isColorChanged == true)
            return;

        if (collision.gameObject.TryGetComponent<Plane>(out _))
        {
            _isColorChanged = true;
            SetColor(Random.ColorHSV());
            StartCoroutine(Deactivate(Random.Range(MinLifetime, MaxLifetime)));
        }
    }
    #endregion

    public void SetPool(ObjectPool<Cube> pool)
    {
        _pool = pool;
    }

    private IEnumerator Deactivate(float delay)
    {
        yield return new WaitForSeconds(delay);

        _pool.Release(this);
    }

    private void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}
