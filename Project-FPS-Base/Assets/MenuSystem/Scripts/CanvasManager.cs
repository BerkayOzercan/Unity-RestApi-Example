using Assets.GameSystem.Scripts;
using Assets.NetworkSystem.SignIn.Scripts;
using TMPro;
using UnityEngine;

namespace Assets.MenuSystem.Scripts
{
    public class CanvasManager : Singleton<CanvasManager>
    {
        [Header("Canvases")]
        public GameObject LogInCanvas = null;
        public GameObject StartCanvas = null;
        public GameObject PauseCanvas = null;
        public GameObject GameCanvas = null;
        public GameObject WinCanvas = null;

        protected override void Awake()
        {
            base.Awake();
        }

        void OnPlayingState(bool value)
        {
            if (value)
                GameCanvas.SetActive(true);
            else
                GameCanvas.SetActive(false);

        }

        void OnStartState(bool value)
        {
            if (value)
                StartCanvas.SetActive(true);
            else
                StartCanvas.SetActive(false);
        }

        void OnWinState(bool value)
        {
            if (value)
                WinCanvas.SetActive(true);
            else
                WinCanvas.SetActive(false);
        }

        void OnPauseState(bool value)
        {
            if (value)
                PauseCanvas.SetActive(true);
            else
                PauseCanvas.SetActive(false);
        }

        void OnLogInState(bool value)
        {
            if (value)
                LogInCanvas.SetActive(true);
            else
                LogInCanvas.SetActive(false);
        }

        void OnEnable()
        {
            GameManager.OnWinState += OnWinState;
            GameManager.OnStartState += OnStartState;
            GameManager.OnPlayState += OnPlayingState;
            GameManager.OnPauseState += OnPauseState;
            GameManager.OnLogInState += OnLogInState;
        }

        void OnDisable()
        {
            GameManager.OnWinState -= OnWinState;
            GameManager.OnStartState -= OnStartState;
            GameManager.OnPlayState -= OnPlayingState;
            GameManager.OnPauseState -= OnPauseState;
            GameManager.OnLogInState -= OnLogInState;
        }
    }
}


