using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.InputSystem;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Managers")]
        [SerializeField]
        private GameInputsManager _gameInputsManager;
        private ScoreManager _scoreManager = null;
        private CanvasManager _canvasManager = null;


        private IGameState currentState;
        private Dictionary<GameStates, IGameState> states;

        protected override void Awake()
        {
            base.Awake();

            _scoreManager = GetComponent<ScoreManager>();
            _canvasManager = GetComponent<CanvasManager>();

            states = new Dictionary<GameStates, IGameState>
            {
                { GameStates.Menu, new MenuState(_canvasManager) },
                { GameStates.Playing, new PlayingState(this, _gameInputsManager, _scoreManager, _canvasManager) },
                { GameStates.Paused, new PausedState(this, _gameInputsManager, _canvasManager) },
                { GameStates.GameOver, new GameOverState() }
            };

            if (_canvasManager.ParentCanvas.activeSelf == false)
                _canvasManager.ParentCanvas.SetActive(true);

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

