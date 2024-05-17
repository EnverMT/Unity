using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Cube), typeof(Collider))]
public class Explosion : MonoBehaviour
{
    private const float InitExplodeForce = 300f;
    private const float InitExplodeRange = 10f;

    private float explodeMultiplier;

    private Cube cube;

    private void OnEnable()
    {
        cube = GetComponent<Cube>();
        cube.Destroying += OnDestroying;
    }

    private void OnDisable()
    {
        cube.Destroying -= OnDestroying;
    }    

    private void OnDestroying(Cube cube)
    {
        this.explodeMultiplier = 1 / cube.ScaleMultiplier;
        if (cube.children.Count > 0)
        {
            this.Explode(cube.children.Select(c => c.GetComponent<Collider>()).ToArray());
        }
        else
        {
            this.Explode(this.GetAffectedCubeObjects());
        }
    }
    private void Explode(Collider[] colliders)
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
        return Physics.OverlapSphere(this.gameObject.transform.position, InitExplodeRange * this.explodeMultiplier).Select(obj => obj.GetComponent<Collider>()).ToArray();
    }
}
