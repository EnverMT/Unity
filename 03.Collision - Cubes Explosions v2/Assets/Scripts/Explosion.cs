using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float InitExplodeForce = 300f;
    private const float InitExplodeRange = 10f;

    public void Explode(Vector3 explosionCenter, float explodeMultiplier, Cube[] cubes)
    {
        Rigidbody[] rigidbodies = cubes.Select(cube => cube.GetComponent<Rigidbody>()).ToArray();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.AddExplosionForce(InitExplodeForce * explodeMultiplier, explosionCenter, InitExplodeRange * explodeMultiplier);
        }
    }

    public void ExpodeInRadius(Vector3 explosionCenter, float explodeMultiplier)
    {
        Cube[] cubesInRadius = GetCubesInRadius(explosionCenter, explodeMultiplier);

        Explode(explosionCenter, explodeMultiplier, cubesInRadius);
    }

    private Cube[] GetCubesInRadius(Vector3 center, float explodeMultiplier)
    {
        List<Cube> cubes = new List<Cube>();
        Collider[] colliders = Physics.OverlapSphere(center, InitExplodeRange * explodeMultiplier).Where(collider => collider.TryGetComponent(out Cube _)).ToArray();

        foreach (var item in colliders)
        {
            Cube cube = item.GetComponent<Cube>();
            cubes.Add(cube);
        }

        return cubes.ToArray();
    }
}
