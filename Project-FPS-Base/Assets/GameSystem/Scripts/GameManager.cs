using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public float Score { get; set; }
        private IGameState currentState;
        private Dictionary<GameStates, IGameState> states;

        protected override void Awake()
        {
            base.Awake();

            states = new Dictionary<GameStates, IGameState>
            {
                { GameStates.Menu, new MenuState() },
                { GameStates.Playing, new PlayingState() },
                { GameStates.Paused, new PausedState() },
                { GameStates.GameOver, new GameOverState() }
            };

            ChangeState(GameStates.Menu);
        }

        private void Update()
        {
            currentState?.OnUpdate();
        }

        public void ChangeState(GameStates newState)
        {
            if (currentState != null)
                currentState.OnExit();

            currentState = states[newState];
            currentState.OnEnter();
        }

        public void AddScore(float score)
        {
            Score += score;
            Debug.Log($"Score: {Score}");
        }
    }
}

