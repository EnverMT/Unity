using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _explodeForce;
    private float _explodeRange;

    public void Init(float explosionMultiplier = 1f)
    {
        _explodeForce = 300f * explosionMultiplier;
        _explodeRange = 10f * explosionMultiplier;
    }

    public void Explode()
    {
        foreach (Collider child in GetAffectedCubeObject())
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody rigid))
            {
                rigid.AddExplosionForce(_explodeForce, transform.position, _explodeRange);
            }
        }
    }

    private Collider[] GetAffectedCubeObject()
    {
        return Physics.OverlapSphere(gameObject.transform.position, _explodeRange).Where(obj => obj.TryGetComponent<Cube>(out _)).ToArray();
    }
}
