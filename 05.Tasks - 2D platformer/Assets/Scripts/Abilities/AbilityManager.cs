using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;


public class AbilityManager : MonoBehaviour
{
    [SerializeField] private BaseUnit _player;
    private Dictionary<KeyCode, BaseAbility> _abilityKeys = new();

    private void OnValidate()
    {
        Assert.IsNotNull(_player);
    }

    private void OnEnable()
    {
        foreach (BaseAbility ability in GetComponentsInChildren<BaseAbility>())
        {
            _abilityKeys.Add(ability.ActivateKey, ability);
        }
    }

    private void OnDisable()
    {
        _abilityKeys.Clear();
    }

    private void Update()
    {
        foreach (KeyValuePair<KeyCode, BaseAbility> item in _abilityKeys)
        {
            if (Input.GetKeyDown(item.Key))
                item.Value.Execute(_player);
        }
    }
}