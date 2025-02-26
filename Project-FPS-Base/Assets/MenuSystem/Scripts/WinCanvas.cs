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

            _nextLevelBtn.onClick.AddListener(() => LevelManager.Instance.LoadNext());
            _restartLevelBtn.onClick.AddListener(() => Debug.Log("RestartLevel"));
            _mainMenuBtn.onClick.AddListener(() => Debug.Log("StartCanvas"));

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


