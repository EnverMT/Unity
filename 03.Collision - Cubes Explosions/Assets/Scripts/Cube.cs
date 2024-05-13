using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Explosion))]
public class Cube : MonoBehaviour
{
    public float SplitChance { get; private set; }

    public delegate GameObject[] CubeDelegate(Cube cube);
    public event CubeDelegate OnSplitting;

    public void Init(float splitChance, Vector3 scale)
    {
        SetRandomColor();
        SplitChance = splitChance;
        gameObject.transform.localScale = scale;
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
            GameObject[] childObjects = OnSplitting?.Invoke(this);

            Explosion explosion = GetComponent<Explosion>();
            explosion.Explode(childObjects);
        }

        Destroy(gameObject);
    }

    private bool CanSplit()
    {
        return Random.value <= SplitChance;
    }
}
