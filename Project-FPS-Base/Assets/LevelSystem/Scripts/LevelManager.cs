using Assets.PlayerSystem.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.LevelSystem.Scripts
{
    public class LevelManager : Singleton<LevelManager>
    {
        const string LAST_SCENE = "Level";
        const string START_MENU = "StartMenu";

        void Start()
        {
            if (PlayerPrefs.GetString(LAST_SCENE) == string.Empty)
            {
                SetScene(1);
                Debug.Log("GET LEVEL IS EMPTY");
            }
        }

        public void LoadStartMenu()
        {
            SceneManager.LoadScene(START_MENU);
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(LAST_SCENE))
            {
                string lastScene = PlayerPrefs.GetString(LAST_SCENE);
                if (lastScene != START_MENU)
                {
                    SceneManager.LoadScene(lastScene);
                    Debug.Log("Game Loaded: " + lastScene);
                    return;
                }
            }
        }

        void SetScene(int value)
        {
            PlayerPrefs.SetString(LAST_SCENE, $"Level {value}");
        }
    }
}
