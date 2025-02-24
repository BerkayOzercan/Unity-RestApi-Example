using System;
using System.Collections.Generic;
using Assets.InputSystem;
using Assets.MenuSystem.Scripts;
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

        private IGameState _currentState;
        private Dictionary<GameStates, IGameState> states;

        public static Action<bool> OnPlayState;
        public static Action<bool> OnWinState;
        public static Action<bool> OnLoseState;
        public static Action<bool> OnPauseState;
        public static Action<bool> OnStartState;
        public static Action<bool> OnLogInState;

        protected override void Awake()
        {
            base.Awake();
            _canvasManager = CanvasManager.Instance;

            states = new Dictionary<GameStates, IGameState>
            {
                { GameStates.LogIn, new LogInState(_canvasManager) },
                { GameStates.Start, new StartState(OnStartState) },
                { GameStates.Play, new PlayState(this, _gameInputsManager, OnPlayState) },
                { GameStates.Pause, new PauseState(this, _gameInputsManager, OnPauseState) },
                {GameStates.Win, new WinState(OnWinState)},
                { GameStates.Lose, new LoseState(OnLoseState) }
            };
        }

        void Start()
        {
            if (_offlineGame)
            {
                ChangeState(GameStates.Start);
            }
            else
            {
                if (!NetworkManager.Instance.IsLoggedIn())
                    ChangeState(GameStates.LogIn);
                else
                    ChangeState(GameStates.Start);
            }
        }

        private void Update()
        {
            _currentState?.OnUpdate();
        }

        /// <summary>
        /// Change game state
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(GameStates newState)
        {
            if (_currentState != null)
                _currentState.OnExit();

            _currentState = states[newState];
            _currentState.OnEnter();
        }

        /// <summary>
        /// Set pause game
        /// </summary>
        public void PauseGame()
        {
            ChangeState(GameStates.Pause);
        }

        /// <summary>
        /// Set game win
        /// </summary>
        public void GameWin()
        {
            ChangeState(GameStates.Win);
        }

        /// <summary>
        /// Set Game Over
        /// </summary>
        public void GameOver()
        {

        }
    }
}

