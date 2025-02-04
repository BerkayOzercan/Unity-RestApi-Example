using Assets.InputSystem;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public enum GameStates
    {
        Menu,
        Playing,
        Paused,
        GameOver
    }

    public class MenuState : IGameState
    {
        private readonly GameManager _gameManager;
        public MenuState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void OnEnter()
        {
            Pause();
            _gameManager.MenuCanvas.SetActive(true);
            Debug.Log(Time.timeScale);
        }
        public void OnUpdate() { }
        public void OnExit()
        {
            ResumeGame();
            _gameManager.MenuCanvas.SetActive(false);
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

    public class PlayingState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameInputsManager _gameInputManager;
        public PlayingState(GameManager gameManager, GameInputsManager gameInputsManager)
        {
            _gameManager = gameManager;
            _gameInputManager = gameInputsManager;
        }

        public void OnEnter()
        {
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
            _gameManager.CrossHairCanvas.SetActive(false);
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            SetCursorState(true);
            _gameInputManager.Escape = false;
            _gameManager.CrossHairCanvas.SetActive(true);
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
        public PausedState(GameManager gameManager, GameInputsManager gameInputsManager)
        {
            _gameManager = gameManager;
            _gameInputManager = gameInputsManager;
        }

        public void OnEnter()
        {
            Pause();
        }
        public void OnUpdate()
        {
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
            _gameManager.pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            _gameManager.pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
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


