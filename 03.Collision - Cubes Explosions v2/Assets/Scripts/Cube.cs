using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Explosion))]
public class Cube : MonoBehaviour
{
    public readonly List<Cube> children = new List<Cube>();

    private readonly Vector3 initScale = Vector3.one;

    public event System.Action<Cube> Clicked;
    public event System.Action<Cube> Destroying;

    public bool CanSplit => Random.value <= this.SplitChance;
    public float SplitChance { get; private set; }
    public float ScaleMultiplier { get; private set; }

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);

        Destroying?.Invoke(this);
        Destroy(this.gameObject);
    }

    public void Init(float splitChance, float scaleMultiplier)
    {
        this.SetRandomColor();
        this.SplitChance = splitChance;
        this.ScaleMultiplier = scaleMultiplier;

        this.gameObject.transform.localScale = this.initScale * scaleMultiplier;
    }

    private void SetRandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value, Random.value);
        this.GetComponent<Renderer>().material.color = randomColor;
    }
}
