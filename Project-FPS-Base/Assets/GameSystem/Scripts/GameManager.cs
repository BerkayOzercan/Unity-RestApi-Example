using System;
using System.Collections.Generic;
using Assets.InputSystem;
using Assets.NetworkSystem;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class GameManager : Singleton<GameManager>
    {

        [Header("Managers")]
        [SerializeField]
        private GameInputsManager _gameInputsManager;
        private CanvasManager _canvasManager = null;

        [Header("Set Game Offline")]
        [SerializeField]
        private bool _offlineGame = false;

        private IGameState currentState;
        private Dictionary<GameStates, IGameState> states;

        public static Action<bool> OnPlayingState;
        public static Action<bool> OnGameWin;
        public static Action<bool> OnGameOnver;
        public static Action<bool> OnGamePause;
        public static Action<bool> OnMenuState;
        public static Action<bool> OnLogInState;

        protected override void Awake()
        {
            base.Awake();
            _canvasManager = GetComponent<CanvasManager>();

            states = new Dictionary<GameStates, IGameState>
            {
                { GameStates.LogIn, new LogInState(_canvasManager) },
                { GameStates.Menu, new MenuState(OnMenuState) },
                { GameStates.Playing, new PlayingState(this, _gameInputsManager, OnPlayingState) },
                { GameStates.Paused, new PausedState(this, _gameInputsManager, OnGamePause) },
                {GameStates.GameWin, new GameWinState(OnGameWin)},
                { GameStates.GameOver, new GameOverState(OnGameOnver) }
            };

            if (_canvasManager.ParentCanvas.activeSelf == false)
                _canvasManager.ParentCanvas.SetActive(true);

            if (_offlineGame)
            {
                ChangeState(GameStates.Menu);
            }
            else
            {
                if (!NetworkManager.Instance.IsLoggedIn())
                    ChangeState(GameStates.LogIn);
                else
                    ChangeState(GameStates.Menu);
            }
        }

        private void Update()
        {
            currentState?.OnUpdate();
        }

        /// <summary>
        /// Change game state
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(GameStates newState)
        {
            if (currentState != null)
                currentState.OnExit();

            currentState = states[newState];
            currentState.OnEnter();
        }

        /// <summary>
        /// Set play game
        /// </summary>
        public void PlayGame()
        {
            ChangeState(GameStates.Playing);
        }

        /// <summary>
        /// Restart  current level
        /// </summary>
        public void RestartGame()
        {
            //Restart level from level manager
        }

        /// <summary>
        /// Set pause game
        /// </summary>
        public void PauseGame()
        {
            ChangeState(GameStates.Paused);
        }

        /// <summary>
        /// Set game win
        /// </summary>
        public void GameWin()
        {
            ChangeState(GameStates.GameWin);
        }

        /// <summary>
        /// Set Game Over
        /// </summary>
        public void GameOver()
        {

        }

        /// <summary>
        /// Quitting Game
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

