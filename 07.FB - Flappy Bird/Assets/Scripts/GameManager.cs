using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace FlappyBird
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Bird _bird;
        [SerializeField] private TextMeshProUGUI _gameOverText;

        private void OnValidate()
        {
            Assert.IsNotNull(_bird, "Bird is not set");
            Assert.IsNotNull(_gameOverText, "Game Over Text is not set");

            foreach (Strings.Scene scene in System.Enum.GetValues(typeof(Strings.Scene)))
            {
                Assert.IsTrue(IsSceneExists(scene), $"Scene '{scene.ToString()}' does not exist in the build settings.");
            }
        }

        private void Start()
        {
            _gameOverText.gameObject.SetActive(false);
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
            Debug.Log(Strings.Scene.GameOver);
            Time.timeScale = 0;
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine(LoadGameOverSceneAfterDelay(5f, Strings.Scene.GameOver));
        }

        private IEnumerator LoadGameOverSceneAfterDelay(float delay, Strings.Scene scene)
        {
            yield return new WaitForSecondsRealtime(delay);
            SceneManager.LoadScene(scene.ToString());
        }

        private bool IsSceneExists(Strings.Scene scene)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                if (sceneFileName == scene.ToString())
                    return true;
            }
            return false;
        }
    }
}