using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Explosion : MonoBehaviour
{
    private const float _explodeForce = 600;
    private const float _explodeRange = 60;

    public void Explode(GameObject[] affectedObjects)
    {
        foreach (GameObject child in affectedObjects)
        {
            child.GetComponent<Rigidbody>().AddExplosionForce(_explodeForce, transform.position, _explodeRange);
        }
    }
}
