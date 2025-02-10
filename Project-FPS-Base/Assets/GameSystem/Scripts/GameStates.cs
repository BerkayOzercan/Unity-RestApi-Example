using Assets.InputSystem;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public enum GameStates
    {
        Menu,
        Playing,
        Paused,
        GameWin,
        GameOver
    }

    public class MenuState : IGameState
    {
        private readonly CanvasManager _canvasManager;
        public MenuState(CanvasManager canvasManager)
        {
            _canvasManager = canvasManager;
        }

        public void OnEnter()
        {
            Pause();
            _canvasManager.MenuCanvas.SetActive(true);
            Debug.Log(Time.timeScale);
        }
        public void OnUpdate() { }
        public void OnExit()
        {
            ResumeGame();
            _canvasManager.MenuCanvas.SetActive(false);
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
        private readonly CanvasManager _canvasManager;
        private readonly ScoreManager _scoreManager;

        public GameWinState(CanvasManager canvasManager, ScoreManager scoreManager)
        {
            _canvasManager = canvasManager;
            _scoreManager = scoreManager;
        }

        public void OnEnter()
        {
            string levelTotalScore = _scoreManager.TotalLevelScore().ToString();
            string levelBonus = _scoreManager.LevelBonus.ToString();
            string levelCurrency = _scoreManager.LevelCurreny.ToString();
            string levelTime = _scoreManager.LevelTime.ToString();

            _canvasManager.SetTotalScore(levelTotalScore, levelBonus, levelCurrency, levelTime);

            Pause();
        }
        public void OnUpdate() { }

        public void OnExit() { _canvasManager.GameWinCanvas.SetActive(false); }

        public void Pause()
        {
            _canvasManager.GameWinCanvas.SetActive(true);
            Time.timeScale = 0f;
        }

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


