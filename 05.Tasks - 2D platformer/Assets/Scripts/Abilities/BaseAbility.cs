using UnityEngine;


public abstract class BaseAbility : MonoBehaviour
{
    public abstract KeyCode ActivateKey { get; }
    public abstract void Execute(BaseUnit player);
}