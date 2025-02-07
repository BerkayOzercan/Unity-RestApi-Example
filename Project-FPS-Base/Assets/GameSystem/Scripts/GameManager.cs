using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.InputSystem;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Canvases")]
        [SerializeField]
        public GameObject GameCanvas = null;
        public GameObject MenuCanvas = null;
        public GameObject pauseCanvas = null;
        public GameObject CrossHairCanvas = null;

        [Header("Managers")]
        [SerializeField]
        private GameInputsManager _gameInputsManager;

        public float Score { get; set; }
        private IGameState currentState;
        private Dictionary<GameStates, IGameState> states;

        protected override void Awake()
        {
            base.Awake();

            states = new Dictionary<GameStates, IGameState>
            {
                { GameStates.Menu, new MenuState(this) },
                { GameStates.Playing, new PlayingState(this, _gameInputsManager) },
                { GameStates.Paused, new PausedState(this, _gameInputsManager) },
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
        /// Get main menu
        /// </summary>
        public void MainMenu()
        {
            ChangeState(GameStates.Menu);
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
        /// Quitting Game
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

