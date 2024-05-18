using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float InitExplodeForce = 300f;
    private const float InitExplodeRange = 10f;

    public void Explode(Vector3 explosionCenter, float explodeMultiplier, Cube[] cubes = null)
    {
        Rigidbody[] rigidbodies;

        if (cubes == null)
        {
            rigidbodies = GetRigidbodiesInRadius(explosionCenter, explodeMultiplier);
        }
        else
        {
            rigidbodies = cubes.Select(cube => cube.GetComponent<Rigidbody>()).ToArray();
        }

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.AddExplosionForce(InitExplodeForce * explodeMultiplier, explosionCenter, InitExplodeRange * explodeMultiplier);
        }
    }

    private Rigidbody[] GetRigidbodiesInRadius(Vector3 center, float explodeMultiplier)
    {
        return Physics.OverlapSphere(center, InitExplodeRange * explodeMultiplier)
            .Where(collider => collider.TryGetComponent(out Cube _))
            .Select(collider => collider.GetComponent<Rigidbody>())
            .ToArray();
    }
}
