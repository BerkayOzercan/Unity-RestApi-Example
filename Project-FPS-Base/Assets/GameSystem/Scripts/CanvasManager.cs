using TMPro;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class CanvasManager : Singleton<CanvasManager>
    {
        [Header("Canvases")]
        public GameObject LogInCanvas = null;
        public GameObject ParentCanvas = null;
        public GameObject MenuCanvas = null;
        public GameObject PauseCanvas = null;
        public GameObject GameCanvas = null;
        public GameObject GameWinCanvas = null;
        public GameObject CrossHairCanvas = null;

        [Header("Score Texts")]
        [SerializeField]
        private TextMeshProUGUI _totalScoreText = null;
        [SerializeField]
        private TextMeshProUGUI _counterTimeText = null;
        [SerializeField]
        private TextMeshProUGUI _currencyText = null;
        [SerializeField]
        private TextMeshProUGUI _bonusText = null;

        void OnGameWin(bool value)
        {
            if (value)
            {
                SetTotalScore(ScoreManager.Instance);
                GameWinCanvas.SetActive(true);
            }
            else
                GameWinCanvas.SetActive(false);

        }

        void SetTotalScore(ScoreManager scoreManager)
        {
            var scoreList = $"Level Score = {scoreManager.GetLevelScore()}\n" +
                            $"Bonus = {scoreManager.LevelBonus}\n" +
                            $"Currency = {scoreManager.LevelCurrency}\n" +
                            $"Time = {scoreManager.Time}\n";

            _totalScoreText.text = scoreList;
        }

        /// <summary>
        /// Set bonus text  
        /// </summary>
        /// <param name="value"></param>
        public void SetBonusText(string value)
        {
            _bonusText.text = $"Bonus: {value}";
        }

        /// <summary>
        /// Set counter time
        /// </summary>
        /// <param name="value"></param>
        public void SetCounterText(string value)
        {
            _counterTimeText.text = $"Time: {value}";
        }

        /// <summary>
        /// Set currency
        /// </summary>
        /// <param name="value"></param>
        public void SetCurrencyText(string value)
        {
            _currencyText.text = $"Collects: {value}";
        }

        void OnEnable()
        {
            GameManager.OnGameWin += OnGameWin;
        }

        void OnDisable()
        {
            GameManager.OnGameWin -= OnGameWin;
        }
    }
}


