using System;
using System.Collections.Generic;
using Assets.InputSystem;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        IGameState _currentState;
        Dictionary<GameStates, IGameState> states;

        public bool IsPause { get; set; }

        public static Action<bool> OnPlayState;
        public static Action OnWinState;
        public static Action<bool> OnLoseState;
        public static Action<bool> OnPauseState;


        protected override void Awake()
        {
            base.Awake();

            states = new Dictionary<GameStates, IGameState>
            {
                { GameStates.Play, new PlayState(this, OnPlayState) },
                { GameStates.Pause, new PauseState(this, OnPauseState) },
                {GameStates.Win, new WinState(OnWinState)},
                { GameStates.Lose, new LoseState(OnLoseState) }
            };
        }

        void Start()
        {
            ChangeState(GameStates.Play);
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

        void OnPause()
        {
            if (IsPause == false)
                IsPause = true;
            else
                IsPause = false;
        }

        void OnEnable()
        {
            Debug.Log("enable");
            GameInputsManager.OnPause += OnPause;
        }

        void OnDisable()
        {
            GameInputsManager.OnPause -= OnPause;
        }
    }
}

