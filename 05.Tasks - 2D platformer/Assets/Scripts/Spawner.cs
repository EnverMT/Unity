using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    private ObjectPool<Coin> _coinPool;

    private void OnValidate()
    {
        Assert.IsNotNull(_coin);
    }


}
