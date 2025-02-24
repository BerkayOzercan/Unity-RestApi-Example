using Assets.GameSystem.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MenuSystem.Scripts
{
    public class WinCanvas : MonoBehaviour
    {
        [SerializeField]
        private Button _nextLevelBtn = null;
        [SerializeField]
        private Button _restartLevelBtn = null;
        [SerializeField]
        private Button _mainMenuBtn = null;
        [SerializeField]
        private TextMeshProUGUI _totalScoreText = null;

        private LevelManager _levelManager = null;
        private GameManager _gameManager = null;
        private ScoreManager _scoreManager = null;
        private CanvasManager _canvasManager = null;

        void Awake()
        {
            _levelManager = LevelManager.Instance;
            _gameManager = GameManager.Instance;
            _scoreManager = ScoreManager.Instance;
            _canvasManager = CanvasManager.Instance;


            _nextLevelBtn.onClick.AddListener(() => _levelManager.LoadNext());
            _restartLevelBtn.onClick.AddListener(() => _levelManager.Reload());
            _mainMenuBtn.onClick.AddListener(() => _gameManager.ChangeState(GameStates.Menu));

            _canvasManager.WinCanvas = gameObject;
        }

        void Start()
        {
            SetScore(_scoreManager);
        }

        void SetScore(ScoreManager scoreManager)
        {
            var scoreList = $"Level Score = {scoreManager.GetLevelScore()}\n" +
                            $"Bonus = {scoreManager.LevelBonus}\n" +
                            $"Currency = {scoreManager.LevelCurrency}\n" +
                            $"Time = {scoreManager.Time}\n";

            _totalScoreText.text = scoreList;
        }
    }
}


