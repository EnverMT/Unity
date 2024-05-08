using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _splitChance = 1;

    private void OnMouseUpAsButton()
    {
        int newCubeCountMin = 2;
        int newCubeCountMax = 6;

        var random = new System.Random();
        int newCubeCount = random.Next(newCubeCountMin, newCubeCountMax + 1);

        if (CanSplit())
        {
            for (global::System.Int32 i = 0; i < newCubeCount; i++)
            {
                SpawnNewCube();
            }
        }

        this._cubeSpawner.DestroyCube(this.gameObject);
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
        GameObject cube = this._cubeSpawner.SpawnCube(PrimitiveType.Cube, transform.localScale, transform.position);

        cube.transform.localScale /= 2;

        cube.GetComponent<Cube>().SetSplitChance(this._splitChance / 2);

        return cube;
    }
}
