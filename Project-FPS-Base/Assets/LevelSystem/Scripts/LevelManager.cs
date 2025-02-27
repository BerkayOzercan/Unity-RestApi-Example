using Assets.GameSystem.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.LevelSystem.Scripts
{
    public class LevelManager : Singleton<LevelManager>
    {
        const string LEVEL_KEY = "Level";

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
            }
        }

        public void LoadNext()
        {
            int currentLevel = PlayerPrefs.GetInt(LEVEL_KEY);
            int nextLevel = currentLevel + 1;
            if (SceneManager.GetActiveScene().buildIndex == nextLevel)
            {
                PlayerPrefs.SetInt(LEVEL_KEY, nextLevel);
                SceneManager.LoadScene(nextLevel);
            }
            else
                SceneManager.LoadScene(1);
        }
    }
}
