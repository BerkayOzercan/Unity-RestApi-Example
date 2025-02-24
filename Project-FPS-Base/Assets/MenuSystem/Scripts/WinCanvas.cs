using Assets.GameSystem.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MenuSystem.Scripts
{
    public class WinCanvas : BaseCanvas
    {
        [SerializeField]
        private Button _nextLevelBtn = null;
        [SerializeField]
        private Button _restartLevelBtn = null;
        [SerializeField]
        private Button _mainMenuBtn = null;
        [SerializeField]
        private TextMeshProUGUI _totalScoreText = null;

        void Start()
        {
            _nextLevelBtn.onClick.AddListener(() => _levelManager.LoadNext());
            _restartLevelBtn.onClick.AddListener(() => _levelManager.Load());
            _mainMenuBtn.onClick.AddListener(() => _gameManager.ChangeState(GameStates.Start));

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


