using TMPro;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class CanvasManager : Singleton<CanvasManager>
    {
        [Header("Canvases")]
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

        /// <summary>
        /// Set score
        /// </summary>
        /// <param name="value"></param>
        public void SetTotalScore(string totalScore, string bonus, string levelCurrency, string levelTime)
        {
            var scoreList = $"Level Score = {totalScore}\n" +
                            $"Bonus = {bonus}\n" +
                            $"Currency = {levelCurrency}\n" +
                            $"Time = {levelTime}\n";

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
    }
}


