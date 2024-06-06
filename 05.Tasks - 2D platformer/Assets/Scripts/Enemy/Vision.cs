using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Vision : MonoBehaviour
{
    [SerializeField] public Player? Target { get; private set; } = null;

    public event Action<Player> TargetFound;
    public event Action<Player> TargetLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Target = player;
            TargetFound?.Invoke(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player) && Target == player)
        {
            Target = null;
            TargetLost?.Invoke(player);
        }
    }
}
