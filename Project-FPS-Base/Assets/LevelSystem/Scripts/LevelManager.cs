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

        /// <summary>
        /// Load the next level if available.
        /// </summary>
        public void LoadNext()
        {
            // string savedLevel = PlayerPrefs.GetString(LEVEL_KEY);

            // if (PlayerPrefs.HasKey(savedLevel))
            // {
            //     PlayerPrefs.SetInt(LEVEL_KEY, nextLevel);
            //     SceneManager.LoadScene(nextLevel);
            // }
            // else
            // {
            //     Debug.Log("This is the last level. Cannot load next.");
            // }
        }
    }
}
