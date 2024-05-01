using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private ParticleSystem _effect;

    private void OnMouseUpAsButton()
    {
        Explode();
        if (_effect) Instantiate(_effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObjects in GetExplodableObjects())
        {
            explodableObjects.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> barrels = new();

        foreach (Collider collider in colliders)
        {
            if (collider.attachedRigidbody != null) barrels.Add(collider.attachedRigidbody);
        }

        return barrels;
    }
}
