using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _splitChance = 1;
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
                SpawnNewCube();
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

    public Cube SetSplitChance(float splitChance)
    {
        this._splitChance = splitChance;
        return this;
    }

    private bool CanSplit()
    {
        var random = new System.Random();

        if (random.NextDouble() < (double)_splitChance)
        {
            return true;
        }

        return false;
    }

    private GameObject SpawnNewCube()
    {
        float radius = 2f;
        Vector3 randomOffset = Random.insideUnitSphere * radius;
        if (randomOffset.y < transform.position.y)
            randomOffset.y *= -1;           // To prevent dropping under terrain

        Vector3 spawnPos = transform.position + randomOffset;

        GameObject cube = this._cubeSpawner.SpawnCube(PrimitiveType.Cube, transform.localScale, spawnPos);

        cube.transform.localScale /= 2;

        cube.GetComponent<Cube>().SetSplitChance(this._splitChance / 2);

        return cube;
    }
}
