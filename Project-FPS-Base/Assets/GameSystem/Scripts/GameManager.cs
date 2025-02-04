using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Canvases")]
        [SerializeField]
        public GameObject GameCanvas = null;
        public GameObject MenuCanvas = null;
        [SerializeField]
        public GameObject CrossHairCanvas = null;

        public float Score { get; set; }
        private IGameState currentState;
        private Dictionary<GameStates, IGameState> states;

        protected override void Awake()
        {
            base.Awake();

            states = new Dictionary<GameStates, IGameState>
            {
                { GameStates.Menu, new MenuState(this) },
                { GameStates.Playing, new PlayingState(this) },
                { GameStates.Paused, new PausedState() },
                { GameStates.GameOver, new GameOverState() }
            };

            if (GameCanvas.activeSelf == false)
                GameCanvas.SetActive(true);

            ChangeState(GameStates.Menu);
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
        /// Add point for current score
        /// </summary>
        /// <param name="score"></param>
        public void AddScore(float score)
        {
            Score += score;
            Debug.Log($"Score: {Score}");
        }

        /// <summary>
        /// Set play game
        /// </summary>
        public void PlayGame()
        {
            ChangeState(GameStates.Playing);
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

