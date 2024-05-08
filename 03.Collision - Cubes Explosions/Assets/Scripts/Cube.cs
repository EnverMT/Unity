using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] public float SplitChance = 1;

    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _explosionRadius = 20;
    [SerializeField] private float _explosionForce = 100;

    private void OnMouseUpAsButton()
    {
        int newCubeCountMin = 2;
        int newCubeCountMax = 6;

        var random = new System.Random();
        int newCubeCount = random.Next(newCubeCountMin, newCubeCountMax + 1);

        if (CanSplit())
        {
            for (int i = 0; i < newCubeCount; i++)
            {
                this._cubeSpawner.SplitCube(gameObject);
            }
            Explosion();
        }

        this._cubeSpawner.DestroyCube(this.gameObject);
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, this._explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<Cube>() != null)
                collider.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    public Cube SetSpawner(CubeSpawner cubeSpawner)
    {
        this._cubeSpawner = cubeSpawner;
        return this;
    }

    private bool CanSplit()
    {
        var random = new System.Random();

        if (random.NextDouble() < (double)SplitChance)
        {
            return true;
        }

        return false;
    }
}
