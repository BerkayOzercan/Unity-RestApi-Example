using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GameSystem.Scripts
{
    public class LevelManager : Singleton<LevelManager>
    {
        private const string LEVEL_KEY = "level";

        /// <summary>
        /// Load the last saved level or default to level 1.
        /// </summary>
        public void Load()
        {
            int savedLevel = PlayerPrefs.GetInt(LEVEL_KEY, 0);  // Default to 0 if not set
            if (IsValidSceneIndex(savedLevel))
            {
                SceneManager.LoadScene(savedLevel);
            }
            else
            {
                Debug.LogWarning("Saved level is invalid. Loading first scene.");
                PlayerPrefs.SetInt(LEVEL_KEY, 0);
                SceneManager.LoadScene(0);
            }
        }

        /// <summary>
        /// Load the next level if available.
        /// </summary>
        public void LoadNext()
        {
            int currentLevel = PlayerPrefs.GetInt(LEVEL_KEY, 0);
            int nextLevel = currentLevel + 1;

            if (IsValidSceneIndex(nextLevel))
            {
                PlayerPrefs.SetInt(LEVEL_KEY, nextLevel);
                SceneManager.LoadScene(nextLevel);
            }
            else
            {
                Debug.Log("This is the last level. Cannot load next.");
            }
        }

        /// <summary>
        /// Reset level progress to the first level.
        /// </summary>
        public void ResetProgress()
        {
            PlayerPrefs.SetInt(LEVEL_KEY, 0);
            Debug.Log("Progress reset. Starting from level 0.");
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// Check if the scene index is valid.
        /// </summary>
        private bool IsValidSceneIndex(int index)
        {
            return index >= 0 && index < SceneManager.sceneCountInBuildSettings;
        }
    }
}
