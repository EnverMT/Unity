using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;


public class InputKeyboard : MonoBehaviour
{
    [SerializeField] private BaseUnit _player;
    [SerializeField] private List<BaseAbility> _abilities = new();

    private Dictionary<KeyCode, BaseAbility> _abilityKeys = new();


    private void OnValidate()
    {
        Assert.IsNotNull(_player);
    }

    private void OnEnable()
    {
        foreach (BaseAbility ability in _abilities)
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