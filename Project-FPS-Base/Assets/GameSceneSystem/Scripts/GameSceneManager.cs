using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GameSceneManager.Scripts
{
    public class GameSceneManager : Singleton<GameSceneManager>
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

        /// <summary>
        /// Return Length of Levels
        /// </summary>
        /// <returns></returns>
        /// TODO Refator 
        public int GetLengthOfLevels()
        {
            // var levelText = PlayerPrefs.GetString(LAST_SCENE);
            // char firstNum, secondNum;
            // if (levelText.Length >= 8)
            // {
            //     firstNum = levelText[6];
            //     secondNum = levelText[7];
            //     return int.Parse(firstNum.ToString() + secondNum.ToString());
            // }
            // else if (levelText.Length >= 7)
            // {
            //     firstNum = levelText[6];
            //     return int.Parse(firstNum.ToString());
            // }
            return SceneManager.sceneCountInBuildSettings - 1;
        }

        void SetScene(int value)
        {
            PlayerPrefs.SetString(LAST_SCENE, $"Level{value}");
        }
    }
}
