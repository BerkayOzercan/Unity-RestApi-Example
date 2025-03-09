using System;
using Assets.GameSystem.Scripts;
using UnityEngine.SceneManagement;

namespace Assets.SaveSystem.Scripts
{
    public class SaveLoadManager : Singleton<SaveLoadManager>
    {
        IDataService _dataService;

        protected override void Awake()
        {
            base.Awake();
            _dataService = new FileDataService(new JsonSerializer());
        }

        void SaveGame()
        {
            GameData gameData = LevelManager.Instance.GameData();
            _dataService.Save(gameData);
        }

        void LoadGame(string gameName)
        {
            GameData gameData = LevelManager.Instance.GameData();

            gameData = _dataService.Load(gameName);

            if (String.IsNullOrWhiteSpace(gameData.Name))
            {
                gameData.Name = "Level 1";
                gameData.Score = 0f;
            }

            SceneManager.LoadScene(gameData.Name);
        }

        void OnEnable()
        {
            GameManager.OnWinState += SaveGame;
        }

        void OnDisable()
        {
            GameManager.OnWinState -= SaveGame;
        }
    }
}