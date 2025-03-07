using Assets.GameSystem.Scripts;
using Assets.GameSceneManager.Scripts;
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
            var levelManager = GameSceneManager.Scripts.GameSceneManager.Instance;
            _nextLevelBtn.onClick.AddListener(() => Debug.Log("Load Next Level"));
            _restartLevelBtn.onClick.AddListener(() => levelManager.Load());
            _mainMenuBtn.onClick.AddListener(() => levelManager.LoadStartMenu());

            SetScore(LevelManager.Instance);
        }

        void SetScore(LevelManager levelManager)
        {
            LevelEntities levelEntities = levelManager.GetEntitiesData();

            var scoreList = $"Level Score = {levelManager.GetScore()}\n" +
                            $"Bonus = {levelEntities.Bonus}\n" +
                            $"Currency = {levelEntities.Currency}\n" +
                            $"Time = {levelManager.Time}\n";

            _totalScoreText.text = scoreList;
        }
    }
}


