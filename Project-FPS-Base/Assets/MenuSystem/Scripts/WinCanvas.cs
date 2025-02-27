using Assets.GameSystem.Scripts;
using Assets.LevelSystem.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MenuSystem.Scripts
{
    public class WinCanvas : MonoBehaviour
    {
        [SerializeField]
        Button _nextLevelBtn = null;
        [SerializeField]
        Button _restartLevelBtn = null;
        [SerializeField]
        Button _mainMenuBtn = null;
        [SerializeField]
        TextMeshProUGUI _totalScoreText = null;

        void Start()
        {
            var levelManager = LevelManager.Instance;
            _nextLevelBtn.onClick.AddListener(() => levelManager.LoadNext());
            _restartLevelBtn.onClick.AddListener(() => levelManager.Load());
            _mainMenuBtn.onClick.AddListener(() => levelManager.LoadStartMenu());

            SetScore(ScoreManager.Instance.GetScoreData());
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


