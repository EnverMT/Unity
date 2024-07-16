using UnityEngine;


public abstract class BaseAbility : MonoBehaviour
{
    public abstract KeyCode ActivateKey { get; }
    public abstract bool CanBeCasted { get; }
    public abstract bool IsChanneling { get; }
    public abstract float RemainingChannelTime { get; }
    public abstract float RemainingCooldown { get; }
    public abstract void Execute(BaseUnit player);
}