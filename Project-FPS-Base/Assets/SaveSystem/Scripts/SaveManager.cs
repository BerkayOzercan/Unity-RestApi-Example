using Assets.GameSystem.Scripts;
using Assets.Scripts;

namespace Assets.SaveSystem.Scripts
{
    public class SaveManager : PersistentSingleton<SaveManager>
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

        public GameData GetGameData()
        {
            return ScoreManager.Instance.GameData();
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