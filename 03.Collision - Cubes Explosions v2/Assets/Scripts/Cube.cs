using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Explosion))]
public class Cube : MonoBehaviour
{
    public float SplitChance { get; private set; }
    public float ScaleMultiplier { get; private set; }

    public delegate Collider[] CubeDelegate(Cube cube);
    public event CubeDelegate OnSplitting;

    private readonly Vector3 _initScale = Vector3.one;
    private Explosion _explosion;

    private void OnMouseUpAsButton()
    {
        if (CanSplit())
        {
            _explosion.Explode(OnSplitting?.Invoke(this));
        }
        else
        {
            _explosion.Explode();
        }

        Destroy(gameObject);
    }

    public void Init(float splitChance, float scaleMultiplier)
    {
        _explosion = GetComponent<Explosion>();
        _explosion.Init(1 / scaleMultiplier);

        SetRandomColor();
        SplitChance = splitChance;
        ScaleMultiplier = scaleMultiplier;

        gameObject.transform.localScale = _initScale * scaleMultiplier;
    }

    private bool CanSplit()
    {
        return Random.value <= SplitChance;
    }

    private void SetRandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value, Random.value);
        GetComponent<Renderer>().material.color = randomColor;
    }
}
