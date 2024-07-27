using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CurrentCount : BaseCount
{
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    protected override void OnValueChanged()
    {
        _textMesh.text = $"Current spawned Cubes = {Spawner.GetCurrentSpawns<Cube>()}\n";
        _textMesh.text += $"Current spawned Bombs = {Spawner.GetCurrentSpawns<Bomb>()}";
    }
}