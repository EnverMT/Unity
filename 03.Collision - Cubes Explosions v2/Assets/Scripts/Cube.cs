using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Explosion))]
public class Cube : MonoBehaviour
{
    private readonly Vector3 initScale = Vector3.one;
    private Explosion explosion;

    public delegate Collider[] CubeDelegate(Cube cube);

    public event CubeDelegate OnSplitting;

    public float SplitChance { get; private set; }

    public float ScaleMultiplier { get; private set; }

    private void OnMouseUpAsButton()
    {
        if (this.CanSplit())
        {
            Collider[] colliders = this.OnSplitting?.Invoke(this);
            this.explosion.Explode(colliders);
        }
        else
        {
            this.explosion.Explode();
        }

        Destroy(this.gameObject);
    }

    public void Init(float splitChance, float scaleMultiplier)
    {
        this.explosion = this.GetComponent<Explosion>();
        this.explosion.Init(1 / scaleMultiplier);

        this.SetRandomColor();
        this.SplitChance = splitChance;
        this.ScaleMultiplier = scaleMultiplier;

        this.gameObject.transform.localScale = this.initScale * scaleMultiplier;
    }

    private bool CanSplit()
    {
        return Random.value <= this.SplitChance;
    }

    private void SetRandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value, Random.value);
        this.GetComponent<Renderer>().material.color = randomColor;
    }
}
