using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    public event UnityAction<GameObject> OnCubeSplit;

    [SerializeField] private float _splitChance;

    public float SplitChance
    {
        get => _splitChance;
        set
        {
            if (value <= 0 || value > 1)
                throw new UnityException("Split chance should be beetwen 0 and 1");

            this._splitChance = value;
        }
    }

    public void SetRandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value, Random.value);
        GetComponent<Renderer>().material.SetColor("_Color", randomColor);
    }

    private void OnMouseUpAsButton()
    {
        if (CanSplit())
            OnCubeSplit?.Invoke(gameObject);

        Destroy(gameObject);
    }

    private bool CanSplit()
    {
        if (Random.value <= SplitChance)
            return true;

        return false;
    }
}
