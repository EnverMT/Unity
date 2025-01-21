using System;
using UnityEngine;

[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(BirdCollissionHandler))]
[RequireComponent(typeof(ScoreCounter))]
public class Bird : MonoBehaviour
{
    private BirdMover _mover;
    private BirdCollissionHandler _handler;
    private ScoreCounter _scoreCounter;

    public event Action GameOver;

    private void Awake()
    {
        _mover = GetComponent<BirdMover>();
        _handler = GetComponent<BirdCollissionHandler>();
        _scoreCounter = GetComponent<ScoreCounter>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += CollissionHandler;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= CollissionHandler;
    }

    private void CollissionHandler(IInteractable interactable)
    {
        if (interactable is Pipe)
            GameOver?.Invoke();

        if (interactable is Ground)
            GameOver?.Invoke();
    }
}