using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float _initExplodeForce = 300f;
    private const float _initExplodeRange = 10f;

    private float _explodeMultiplier;

    public void Init(float explosionMultiplier = 1f)
    {
        _explodeMultiplier = explosionMultiplier;
    }

    public void Explode()
    {
        Explode(GetAffectedCubeObjects());
    }

    public void Explode(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_initExplodeForce * _explodeMultiplier, transform.position, _initExplodeRange * _explodeMultiplier);
            }
        }
    }

    private Collider[] GetAffectedCubeObjects()
    {
        return Physics.OverlapSphere(gameObject.transform.position, _initExplodeRange * _explodeMultiplier).Where(obj => obj.TryGetComponent<Cube>(out _)).ToArray();
    }
}
