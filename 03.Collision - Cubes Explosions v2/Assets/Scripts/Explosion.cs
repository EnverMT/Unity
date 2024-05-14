using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float ExplodeForce { get; private set; } = 300f;
    public float ExplodeRange { get; private set; } = 10;

    public void Explode(Collider[] affectedObjects, float explosionMultiplier)
    {     
        foreach (Collider child in affectedObjects)
        {
            if (child.TryGetComponent<Cube>(out _) && child.TryGetComponent<Rigidbody>(out Rigidbody rigid))
            {
                rigid.AddExplosionForce(ExplodeForce * explosionMultiplier, transform.position, ExplodeRange * explosionMultiplier);
            }
        }
    }
}
