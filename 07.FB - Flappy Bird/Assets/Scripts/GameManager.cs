using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Bird _bird;

    private void OnValidate()
    {
        Assert.IsNotNull<Bird>(_bird, "Bird is not set");
    }

    private void OnEnable()
    {
        _bird.GameOver += GameOverHandler;
    }

    private void OnDisable()
    {
        _bird.GameOver -= GameOverHandler;
    }

    private void GameOverHandler()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }
}
