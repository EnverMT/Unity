using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    [SerializeField] public int Value { get; private set; }

    public event Action<Coin, Player> Collected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Heal(Value);
            Destroy(gameObject);
        }
    }

    public void Init(int value)
    {
        Value = value;
    }
}
