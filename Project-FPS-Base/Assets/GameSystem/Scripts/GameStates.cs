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

    public class PlayingState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameInputsManager _gameInputManager;
        private readonly ScoreManager _scoreManager;
        private readonly CanvasManager _canvasManager;

        public PlayingState(GameManager gameManager, GameInputsManager gameInputsManager, ScoreManager scoreManager, CanvasManager canvasManager)
        {
            _gameManager = gameManager;
            _canvasManager = canvasManager;
            _gameInputManager = gameInputsManager;
            _scoreManager = scoreManager;
        }

        public void OnEnter()
        {
            Debug.Log("Playing State");
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
            _canvasManager.CrossHairCanvas.SetActive(false);
            _canvasManager.GameCanvas.SetActive(false);
            _scoreManager.StopTimer();
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            SetCursorState(true);
            _gameInputManager.Escape = false;
            _canvasManager.CrossHairCanvas.SetActive(true);
            _canvasManager.GameCanvas.SetActive(true);
            _scoreManager.StartTimer();
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    public class PausedState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameInputsManager _gameInputManager;
        private readonly CanvasManager _canvasManager;
        public PausedState(GameManager gameManager, GameInputsManager gameInputsManager, CanvasManager canvasManager)
        {
            _gameManager = gameManager;
            _gameInputManager = gameInputsManager;
            _canvasManager = canvasManager;
        }

        public void OnEnter()
        {
            Debug.Log("PauseState");
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
            ResumeGame();
        }
        public void Pause()
        {
            _gameInputManager.Escape = false;
            _canvasManager.PauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            _canvasManager.PauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }

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

    public class GameOverState : IGameState
    {
        public void OnEnter() => Debug.Log("Entered Game Over State");
        public void OnUpdate() { /* Handle game over logic */ }
        public void OnExit() => Debug.Log("Exited Game Over State");
        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        public void ResumeGame()
        {
            throw new System.NotImplementedException();
        }
    }
}


