using System;
using UnityEngine;
using UnityEngine.Assertions;


[RequireComponent(typeof(Collider2D))]
public class BirdCollissionHandler : MonoBehaviour
{
    public event Action<IInteractable> CollisionDetected;

    private void OnValidate()
    {
        Assert.IsTrue(GetComponent<Collider2D>().isTrigger);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
            CollisionDetected?.Invoke(interactable);
    }
}