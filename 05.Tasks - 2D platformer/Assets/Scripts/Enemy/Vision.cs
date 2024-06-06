using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Vision : MonoBehaviour
{
#nullable enable
    private Player? _target;
#nullable disable

    public event Action<Player> TargetFound;
    public event Action<Player> TargetLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _target = player;
            TargetFound?.Invoke(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player) && _target == player)
        {
            _target = null;
            TargetLost?.Invoke(player);
        }
    }
}
