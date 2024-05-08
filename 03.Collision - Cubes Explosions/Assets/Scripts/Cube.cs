using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    public event UnityAction<GameObject> OnCubeSplit;

    [SerializeField] private float _splitChance;

    public void SetSplitChance(float splitChance)
    {
        if (splitChance <= 0 || splitChance > 1)
            new UnityException("Split chance should be beetwen 0 and 1");

        this._splitChance = splitChance;
    }

    private void OnMouseUpAsButton()
    {
        if (CanSplit())
            this.OnCubeSplit?.Invoke(this.gameObject);

        Destroy(gameObject);
    }

    private bool CanSplit()
    {
        if (Random.value < _splitChance)
            return true;

        return false;
    }
}
