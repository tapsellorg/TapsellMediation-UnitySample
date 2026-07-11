using UnityEngine;

namespace Sample.Scripts
{
    public class SceneManager : MonoBehaviour
    {
        private const string MainSceneName = "MainScene";

        public void ChangeScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                if (currentScene != MainSceneName)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(MainSceneName);
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }
}
