using Assets.GameSystem.Scripts;
using UnityEngine;

namespace Assets.MenuSystem.Scripts
{
    public class CanvasManager : MonoBehaviour
    {
        [Header("Canvases")]
        public GameObject PauseCanvas = null;
        public GameObject GameCanvas = null;
        public GameObject WinCanvas = null;

        void OnPlayingState(bool value)
        {
            GameCanvas.SetActive(value);
        }

        void OnWinState()
        {
            WinCanvas.SetActive(true);
        }

        void OnPauseState(bool value)
        {
            PauseCanvas.SetActive(value);
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


