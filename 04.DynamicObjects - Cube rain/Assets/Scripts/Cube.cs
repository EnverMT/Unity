using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    public void Init(float splitChance, float scaleMultiplier)
    {
        SetRandomColor();        
    }

    private void SetRandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value, Random.value);
        GetComponent<Renderer>().material.color = randomColor;
    }
}
