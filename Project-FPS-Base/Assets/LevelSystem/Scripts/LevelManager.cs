using Assets.GameSystem.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.LevelSystem.Scripts
{
    public class LevelManager : Singleton<LevelManager>
    {
        const string LEVEL_KEY = "Level";

        void Start()
        {
            // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // int currentLevelIndex = PlayerPrefs.GetInt(LEVEL_KEY);

        }

        public void LoadStartMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Load()
        {
            int savedLevel = PlayerPrefs.GetInt(LEVEL_KEY);
            if (savedLevel <= 0)
            {
                PlayerPrefs.SetInt(LEVEL_KEY, 1);
                SceneManager.LoadScene(savedLevel);
            }
            else
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt(LEVEL_KEY));
                Debug.Log("On Load" + SceneManager.GetActiveScene().name);
            }
        }

        public void LoadNext()
        {
            int currentLevel = PlayerPrefs.GetInt(LEVEL_KEY);
            int nextLevel = currentLevel + 1;
            Debug.Log(nextLevel);
            if (SceneManager.GetActiveScene().buildIndex < nextLevel)
            {
                PlayerPrefs.SetInt(LEVEL_KEY, nextLevel);
                SceneManager.LoadScene(nextLevel);
            }
            else
                SceneManager.LoadScene(1);
        }
    }
}
