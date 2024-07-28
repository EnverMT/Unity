using UnityEngine;

public class BombSpawner : BaseSpawner<Bomb>
{
    [SerializeField] private Bomb _prefab;

    public override Bomb Prefab => _prefab;
}
