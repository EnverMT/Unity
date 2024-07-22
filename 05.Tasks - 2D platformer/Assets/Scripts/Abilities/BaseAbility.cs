using System;
using UnityEngine;


public abstract class BaseAbility : MonoBehaviour
{
    public abstract KeyCode ActivateKey { get; }
    public abstract bool CanBeCasted { get; }
    public abstract bool IsCooldowning { get; protected set; }
    public abstract float Cooldown { get; protected set; }
    public abstract float RemainingCooldown { get; }

    public abstract event Action ValueChanged;

    public abstract void Execute(BaseUnit player);
}