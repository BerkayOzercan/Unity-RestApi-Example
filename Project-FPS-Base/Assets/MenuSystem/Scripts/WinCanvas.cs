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

            SetScore(_scoreManager.GetScoreData());
        }

        void SetScore(ScoreDto score)
        {
            var scoreList = $"Level Score = {score.LevelScore}\n" +
                            $"Bonus = {score.Bonus}\n" +
                            $"Currency = {score.Currency}\n" +
                            $"Time = {score.Time}\n";

            _totalScoreText.text = scoreList;
        }
    }
}


