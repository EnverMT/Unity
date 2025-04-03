using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace FlappyBird
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Bird _bird;
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private GameSceneManager _gameSceneManager;

        private void OnValidate()
        {
            Assert.IsNotNull(_bird, "Bird is not set");
            Assert.IsNotNull(_gameOverText, "Game Over Text is not set");
            Assert.IsNotNull(_gameSceneManager, "Game Scene Manager is not set");
        }

        private void Start()
        {
            _gameOverText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _bird.Died += GameOver;
        }

        private void OnDisable()
        {
            _bird.Died -= GameOver;
        }

        private void GameOver()
        {
            Debug.Log(Strings.Scene.GameOver);
            Time.timeScale = 0;
            _gameOverText.gameObject.SetActive(true);
            _gameSceneManager.LoadScene(Strings.Scene.GameOver, 2f);
        }
    }
}