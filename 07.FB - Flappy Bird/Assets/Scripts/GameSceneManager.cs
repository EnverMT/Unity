using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace FlappyBird
{
    public class GameSceneManager : MonoBehaviour
    {
        private void OnValidate()
        {
            foreach (Strings.Scene scene in System.Enum.GetValues(typeof(Strings.Scene)))
                Assert.IsTrue(IsSceneExists(scene), $"Scene '{scene.ToString()}' does not exist in the build settings.");
        }

        public void LoadScene(Strings.Scene scene, float delay = 0)
        {
            StartCoroutine(LoadSceneAsync(scene, delay));
        }

        private IEnumerator LoadSceneAsync(Strings.Scene scene, float delay = 0)
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
