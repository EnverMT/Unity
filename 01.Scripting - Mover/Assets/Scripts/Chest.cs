using UnityEngine;

public class Chest : MonoBehaviour
{
    private readonly int OpenTrigger = Animator.StringToHash("Open");

    [SerializeField] private Animator _animator;

    public void Open()
    {
        Debug.Log($"OpenTriggerHashInt={OpenTrigger}");
        _animator.SetTrigger(OpenTrigger);
    }
}
