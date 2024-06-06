using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.Assertions;


public class Enemy : BaseUnit
{
    [SerializeField] private Vision _vision;
    public override bool HasJumpAbility { get => false; }

    private void OnValidate()
    {
        Assert.IsNotNull(_vision);
    }

    private void OnEnable()
    {
        _vision.TargetFound += Pursue;
        _vision.TargetLost += StopPursue;
    }

    private void OnDisable()
    {
        _vision.TargetFound -= Pursue;
        _vision.TargetLost -= StopPursue;
    }

    private void Pursue(Player player)
    {
        Debug.Log($"{gameObject.transform.position} start pursue");
    }

    private void StopPursue(Player player)
    {
        Debug.Log($"{gameObject.transform.position} stop pursue");
    }
}
