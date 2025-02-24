using TMPro;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class CanvasManager : Singleton<CanvasManager>
    {
        [Header("Canvases")]
        public GameObject LogInCanvas = null;
        public GameObject StartCanvas = null;
        public GameObject PauseCanvas = null;
        public GameObject GameCanvas = null;
        public GameObject WinCanvas = null;

        [Header("Score Texts")]
        [SerializeField]
        private TextMeshProUGUI _counterTimeText = null;
        [SerializeField]
        private TextMeshProUGUI _currencyText = null;
        [SerializeField]
        private TextMeshProUGUI _bonusText = null;

        void OnPlayingState(bool value)
        {
            if (value)
            {
                GameCanvas.SetActive(true);
            }
            else
            {
                GameCanvas.SetActive(false);
            }
        }

        void OnMenuState(bool value)
        {
            if (value)
                StartCanvas.SetActive(true);
            else
                StartCanvas.SetActive(false);
        }

        void OnGameWin(bool value)
        {
            if (value)
            {
                WinCanvas.SetActive(true);
            }
            else
                WinCanvas.SetActive(false);

        }

        void OnGamePause(bool value)
        {
            if (value)
                PauseCanvas.SetActive(true);
            else
                PauseCanvas.SetActive(false);
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
            GameManager.OnMenuState += OnMenuState;
            GameManager.OnPlayingState += OnPlayingState;
            GameManager.OnGamePause += OnGamePause;
        }

        void OnDisable()
        {
            GameManager.OnGameWin -= OnGameWin;
            GameManager.OnMenuState -= OnMenuState;
            GameManager.OnPlayingState -= OnPlayingState;
            GameManager.OnGamePause -= OnGamePause;
        }
    }
}


