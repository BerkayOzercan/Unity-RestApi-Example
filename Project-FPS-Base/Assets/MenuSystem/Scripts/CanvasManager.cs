using Assets.GameSystem.Scripts;
using UnityEngine;

namespace Assets.MenuSystem.Scripts
{
    public class CanvasManager : Singleton<CanvasManager>
    {
        [Header("Canvases")]
        public GameObject PauseCanvas = null;
        public GameObject GameCanvas = null;
        public GameObject WinCanvas = null;


        void OnPlayingState(bool value)
        {
            if (value)
                GameCanvas.SetActive(true);
            else
                GameCanvas.SetActive(false);

        }

        void OnWinState()
        {
            WinCanvas.SetActive(true);
        }

        void OnPauseState(bool value)
        {
            if (value)
                PauseCanvas.SetActive(true);
            else
                PauseCanvas.SetActive(false);
        }

        void OnEnable()
        {
            GameManager.OnWinState += OnWinState;
            GameManager.OnPlayState += OnPlayingState;
            GameManager.OnPauseState += OnPauseState;
        }

        void OnDisable()
        {
            GameManager.OnWinState -= OnWinState;
            GameManager.OnPlayState -= OnPlayingState;
            GameManager.OnPauseState -= OnPauseState;
        }


    }
}


