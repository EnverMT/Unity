using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float InitExplodeForce = 300f;
    private const float InitExplodeRange = 10f;

    private float explodeMultiplier;

    public void Init(float explosionMultiplier = 1f)
    {
        this.explodeMultiplier = explosionMultiplier;
    }

    public void Explode()
    {
        this.Explode(this.GetAffectedCubeObjects());
    }

    public void Explode(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(InitExplodeForce * this.explodeMultiplier, this.transform.position, InitExplodeRange * this.explodeMultiplier);
            }
        }
    }

    private Collider[] GetAffectedCubeObjects()
    {
        return Physics.OverlapSphere(this.gameObject.transform.position, InitExplodeRange * this.explodeMultiplier).Where(obj => obj.TryGetComponent<Cube>(out _)).ToArray();
    }
}
