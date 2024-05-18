using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private readonly Vector3 _initScale = Vector3.one;

    public bool CanSplit => Random.value <= this.SplitChance;
    public float SplitChance { get; private set; }
    public float ScaleMultiplier { get; private set; }

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
}
