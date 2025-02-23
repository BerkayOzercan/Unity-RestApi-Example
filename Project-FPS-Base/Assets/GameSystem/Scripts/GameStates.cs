using System;
using Assets.InputSystem;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public enum GameStates
    {
        LogIn,
        Menu,
        Playing,
        Paused,
        GameWin,
        GameOver
    }

    public class LogInState : IGameState
    {
        private readonly CanvasManager _canvasManager;
        public LogInState(CanvasManager canvasManager)
        {
            _canvasManager = canvasManager;
        }

        public void OnEnter()
        {
            _canvasManager.LogInCanvas.SetActive(true);
            Pause();
        }
        public void OnUpdate() { }
        public void OnExit()
        {
            _canvasManager.LogInCanvas.SetActive(false);
            ResumeGame();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }
    }

    #region MenuState
    public class MenuState : IGameState
    {
        private Action<bool> _onMenuState;

        public MenuState(Action<bool> OnMenuState) { _onMenuState = OnMenuState; }

        public void OnEnter() { _onMenuState?.Invoke(true); Pause(); }
        public void OnUpdate() { }
        public void OnExit() { _onMenuState?.Invoke(false); ResumeGame(); }
        public void Pause() { Time.timeScale = 0f; }
        public void ResumeGame() { Time.timeScale = 1f; }
    }
    #endregion

    #region PlayingState
    public class PlayingState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameInputsManager _gameInputManager;
        private readonly Action<bool> _onPLaying;

        public PlayingState(GameManager gameManager, GameInputsManager gameInputsManager, Action<bool> onPlaying)
        {
            _gameManager = gameManager;
            _gameInputManager = gameInputsManager;
            _onPLaying = onPlaying;
        }

        public void OnEnter()
        {
            _onPLaying?.Invoke(true);
            ResumeGame();
        }
        public void OnUpdate()
        {
            if (_gameInputManager.Escape)
                _gameManager.PauseGame();
        }

        public void OnExit()
        {
            Pause();
        }
        public void Pause()
        {
            Time.timeScale = 0f;
            SetCursorState(false);
            _onPLaying?.Invoke(false);
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            SetCursorState(true);
            _gameInputManager.Escape = false;
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
    #endregion


    public class PausedState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameInputsManager _gameInputManager;
        private readonly Action<bool> _onPause;
        public PausedState(GameManager gameManager, GameInputsManager gameInputsManager, Action<bool> onPause)
        {
            _gameManager = gameManager;
            _gameInputManager = gameInputsManager;
            _onPause = onPause;
        }

        public void OnEnter()
        {
            _onPause?.Invoke(true);
            Pause();
        }

        public void OnUpdate()
        {
            if (_gameInputManager == null) return;
            if (_gameInputManager.Escape)
            {
                _gameManager.ChangeState(GameStates.Playing);
            }
        }

        public void OnExit()
        {
            _onPause?.Invoke(false);
            ResumeGame();
        }
        public void Pause()
        {
            _gameInputManager.Escape = false;
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }
    }

    #region GameWinState
    public class GameWinState : IGameState
    {
        private readonly Action<bool> _onGameWin;
        public GameWinState(Action<bool> onGameWin) { _onGameWin = onGameWin; }

        public void OnEnter() { _onGameWin?.Invoke(true); Pause(); }
        public void OnUpdate() { }
        public void OnExit() { _onGameWin?.Invoke(false); }
        public void Pause() { Time.timeScale = 0f; }
        public void ResumeGame() { }
    }
    #endregion

    #region GameOverState
    public class GameOverState : IGameState
    {
        private readonly Action<bool> _onGameOver;
        public GameOverState(Action<bool> onGameOver) { _onGameOver = onGameOver; }
        public void OnEnter() { _onGameOver?.Invoke(true); }
        public void OnUpdate() { }
        public void OnExit() { _onGameOver?.Invoke(false); }
        public void Pause() { }
        public void ResumeGame() { }
    }
    #endregion
}


