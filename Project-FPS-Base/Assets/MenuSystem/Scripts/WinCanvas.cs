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
            _nextLevelBtn.onClick.AddListener(() => Debug.Log("Load Next Level"));
            _restartLevelBtn.onClick.AddListener(() => levelManager.Load());
            _mainMenuBtn.onClick.AddListener(() => levelManager.LoadStartMenu());

            SetScore(ScoreManager.Instance);
        }

        void SetScore(ScoreManager scoreManager)
        {
            ScoreEntities levelEntities = scoreManager.GetEntitiesData();

            var scoreList = $"Level Score = {scoreManager.GetScore()}\n" +
                            $"Bonus = {levelEntities.Bonus}\n" +
                            $"Currency = {levelEntities.Currency}\n" +
                            $"Time = {scoreManager.Time}\n";

            _totalScoreText.text = scoreList;
        }
    }
}


