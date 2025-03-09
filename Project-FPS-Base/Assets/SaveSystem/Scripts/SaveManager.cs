using Assets.GameSystem.Scripts;

namespace Assets.SaveSystem.Scripts
{
    public class SaveManager : Singleton<SaveManager>
    {
        IDataService _dataService;

        protected override void Awake()
        {
            base.Awake();
            _dataService = new FileDataService(new JsonSerializer());
        }

        void SaveGame()
        {
            GameData gameData = ScoreManager.Instance.GameData();
            _dataService.Save(gameData);
        }

        GameData GetGameData(string gameName)
        {
            GameData gameData = ScoreManager.Instance.GameData();

            gameData = _dataService.Load(gameName);

            return gameData;
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