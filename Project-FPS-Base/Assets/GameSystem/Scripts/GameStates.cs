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

        public void OnUpdate() { /* Handle menu logic */ }
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
        public void OnEnter()
        {
            SetCursorState(true);
            ResumeGame();
        }
        public void OnUpdate() { /* Handle game logic */ }
        public void OnExit()
        {
            SetCursorState(false);
            Pause();
        }
        public void Pause()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    public class PausedState : IGameState
    {
        public void OnEnter() => Debug.Log("Entered Paused State");
        public void OnUpdate() { /* Handle pause menu */ }
        public void OnExit() => Debug.Log("Exited Paused State");
        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        public void ResumeGame()
        {
            throw new System.NotImplementedException();
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


