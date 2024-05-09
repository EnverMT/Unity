using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    public float SplitChance { get; private set; }

    public event UnityAction<Cube> OnSplitting;

    public void Init(float splitChance, Vector3 scale)
    {
        SetRandomColor();
        SplitChance = splitChance;
        gameObject.transform.localScale = scale;
    }

    private void SetRandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value, Random.value);
        GetComponent<Renderer>().material.SetColor("_Color", randomColor);
    }

    private void OnMouseUpAsButton()
    {
        if (CanSplit())
            OnSplitting?.Invoke(this);

        Destroy(gameObject);
    }

    private bool CanSplit()
    {
        return Random.value <= SplitChance;
    }
}
