using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GameSystem.Scripts
{
    public class LevelManager : Singleton<LevelManager>
    {
        private int _currentLevel = 1;

        /// <summary>
        /// Load last level
        /// </summary>
        public void Load()
        {
            SceneManager.LoadScene(_currentLevel);
        }

        /// <summary>
        /// Reload current level
        /// </summary>
        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Load next level
        /// </summary>
        public void LoadNext()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            _currentLevel += 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(Random.Range(0, SceneManager.sceneCount));
            }
        }

        /// <summary>
        /// Load previous level
        /// </summary>
        public void LoadPrevious()
        {
            int prevSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
            _currentLevel -= 1;

            if (prevSceneIndex >= 0)
            {
                SceneManager.LoadScene(prevSceneIndex);
            }
            else
            {
                Debug.LogWarning("No previous level available!");
            }
        }
    }
}

