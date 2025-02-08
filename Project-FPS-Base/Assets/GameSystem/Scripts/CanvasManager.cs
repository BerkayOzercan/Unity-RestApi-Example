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
        private TextMeshProUGUI _scoreText = null;
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
        public void SetScoreText(string value)
        {
            _scoreText.text = value;
        }

        /// <summary>
        /// Set bonus text
        /// </summary>
        /// <param name="value"></param>
        public void SetBonusText(string value)
        {
            _bonusText.text = value;
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
            _currencyText.text = value;
        }
    }
}


