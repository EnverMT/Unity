using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TotalCount : BaseCount
{
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    protected override void OnValueChanged()
    {
        _textMesh.text = $"Total spawned Cubes = {Spawner.GetTotalSpawns<Cube>()}\n";
        _textMesh.text += $"Total spawned Bombs = {Spawner.GetTotalSpawns<Bomb>()}";
    }
}
