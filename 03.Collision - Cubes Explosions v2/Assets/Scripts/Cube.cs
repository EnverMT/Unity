using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Explosion))]
public class Cube : MonoBehaviour
{
    public float SplitChance { get; private set; }
    public float ScaleMultiplier { get; private set; }

    public delegate GameObject[] CubeDelegate(Cube cube);
    public event CubeDelegate OnSplitting;

    private readonly Vector3 _initScale = Vector3.one;

    public void Init(float splitChance, float scaleMultiplier)
    {
        SetRandomColor();
        SplitChance = splitChance;
        ScaleMultiplier = scaleMultiplier;

        gameObject.transform.localScale = _initScale * scaleMultiplier;
    }

    private void SetRandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value, Random.value);
        GetComponent<Renderer>().material.color = randomColor;
    }

    private void OnMouseUpAsButton()
    {
        if (CanSplit())
        {
            OnSplitting?.Invoke(this);
        }

        Explosion explosion = GetComponent<Explosion>();
        float explodeRadius = explosion.ExplodeRange / ScaleMultiplier;
        float expldoeForceMultiplier = 1 / ScaleMultiplier;

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, explodeRadius);
        explosion.Explode(colliders, expldoeForceMultiplier);

        Destroy(gameObject);
    }

    private bool CanSplit()
    {
        return Random.value <= SplitChance;
    }
}
