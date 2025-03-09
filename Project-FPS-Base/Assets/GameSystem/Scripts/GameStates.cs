using System;
using Assets.InputSystem;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public enum GameStates
    {
        Play,
        Pause,
        Win,
        Lose
    }

    #region PlayingState
    public class PlayState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly Action<bool> _onPLaying;

        public PlayState(GameManager gameManager, Action<bool> onPlaying)
        {
            _gameManager = gameManager;
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
            if (_gameManager.IsPause)
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
        private readonly Action<bool> _onPause;
        public PauseState(GameManager gameManager, Action<bool> onPause)
        {
            _gameManager = gameManager;
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
            if (_gameManager.IsPause == false)
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
        private readonly Action _onGameWin;
        public WinState(Action onGameWin) { _onGameWin = onGameWin; }

        public void OnEnter() { _onGameWin?.Invoke(); Pause(); Debug.Log("WinState"); }
        public void OnUpdate() { }
        public void OnExit() { _onGameWin?.Invoke(); }
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


