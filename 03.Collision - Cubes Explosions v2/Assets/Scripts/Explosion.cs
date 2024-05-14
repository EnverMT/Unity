using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float _initExplodeForce = 300f;
    private const float _initExplodeRange = 10f;

    private float _explodeForce;
    private float _explodeRange;

    public void Init(float explosionMultiplier = 1f)
    {
        _explodeForce = _initExplodeForce * explosionMultiplier;
        _explodeRange = _initExplodeRange * explosionMultiplier;
    }

    public void Explode()
    {
        foreach (Collider child in GetAffectedCubeObjects())
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody rigid))
            {
                rigid.AddExplosionForce(_explodeForce, transform.position, _explodeRange);
            }
        }
    }

    private Collider[] GetAffectedCubeObjects()
    {
        return Physics.OverlapSphere(gameObject.transform.position, _explodeRange).Where(obj => obj.TryGetComponent<Cube>(out _)).ToArray();
    }
}
