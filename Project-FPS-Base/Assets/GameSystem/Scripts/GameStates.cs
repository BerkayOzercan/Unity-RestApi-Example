using System;
using Assets.InputSystem;
using Assets.MenuSystem.Scripts;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public enum GameStates
    {
        LogIn,
        Start,
        Play,
        Pause,
        Win,
        Lose
    }

    public class LogInState : IGameState
    {
        private readonly Action<bool> _onLogInState;
        public LogInState(Action<bool> onLogInState)
        {
            _onLogInState = onLogInState;
        }

        public void OnEnter()
        {
            _onLogInState?.Invoke(true);
            Pause();
        }
        public void OnUpdate() { }
        public void OnExit()
        {
            _onLogInState?.Invoke(false);
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
    public class StartState : IGameState
    {
        private Action<bool> _onMenuState;

        public StartState(Action<bool> OnMenuState) { _onMenuState = OnMenuState; }

        public void OnEnter() { _onMenuState?.Invoke(true); Pause(); Debug.Log("StartState"); }
        public void OnUpdate() { }
        public void OnExit() { _onMenuState?.Invoke(false); ResumeGame(); }
        public void Pause() { Time.timeScale = 0f; }
        public void ResumeGame() { Time.timeScale = 1f; }
    }
    #endregion

    #region PlayingState
    public class PlayState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameInputsManager _gameInputManager;
        private readonly Action<bool> _onPLaying;

        public PlayState(GameManager gameManager, GameInputsManager gameInputsManager, Action<bool> onPlaying)
        {
            _gameManager = gameManager;
            _gameInputManager = gameInputsManager;
            _onPLaying = onPlaying;
        }

        public void OnEnter()
        {
            _onPLaying?.Invoke(true);
            ResumeGame();
            Debug.Log("PlayingState");
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


    public class PauseState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameInputsManager _gameInputManager;
        private readonly Action<bool> _onPause;
        public PauseState(GameManager gameManager, GameInputsManager gameInputsManager, Action<bool> onPause)
        {
            _gameManager = gameManager;
            _gameInputManager = gameInputsManager;
            _onPause = onPause;
        }

        public void OnEnter()
        {
            _onPause?.Invoke(true);
            Pause();
            Debug.Log("PausedState");
        }

        public void OnUpdate()
        {
            if (_gameInputManager == null) return;
            if (_gameInputManager.Escape)
            {
                _gameManager.ChangeState(GameStates.Play);
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
    public class WinState : IGameState
    {
        private readonly Action<bool> _onGameWin;
        public WinState(Action<bool> onGameWin) { _onGameWin = onGameWin; }

        public void OnEnter() { _onGameWin?.Invoke(true); Pause(); Debug.Log("WinState"); }
        public void OnUpdate() { }
        public void OnExit() { _onGameWin?.Invoke(false); }
        public void Pause() { Time.timeScale = 0f; }
        public void ResumeGame() { }
    }
    #endregion

    #region GameOverState
    public class LoseState : IGameState
    {
        private readonly Action<bool> _onGameOver;
        public LoseState(Action<bool> onGameOver) { _onGameOver = onGameOver; }
        public void OnEnter() { _onGameOver?.Invoke(true); }
        public void OnUpdate() { }
        public void OnExit() { _onGameOver?.Invoke(false); }
        public void Pause() { }
        public void ResumeGame() { }
    }
    #endregion
}


