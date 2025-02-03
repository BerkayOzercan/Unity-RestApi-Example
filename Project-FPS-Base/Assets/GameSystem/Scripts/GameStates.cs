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
        public void OnEnter() => Debug.Log("Entered Menu State");
        public void OnUpdate() { /* Handle menu logic */ }
        public void OnExit() => Debug.Log("Exited Menu State");
    }

    public class PlayingState : IGameState
    {
        public void OnEnter() => Debug.Log("Entered Playing State");
        public void OnUpdate() { /* Handle game logic */ }
        public void OnExit() => Debug.Log("Exited Playing State");
    }

    public class PausedState : IGameState
    {
        public void OnEnter() => Debug.Log("Entered Paused State");
        public void OnUpdate() { /* Handle pause menu */ }
        public void OnExit() => Debug.Log("Exited Paused State");
    }

    public class GameOverState : IGameState
    {
        public void OnEnter() => Debug.Log("Entered Game Over State");
        public void OnUpdate() { /* Handle game over logic */ }
        public void OnExit() => Debug.Log("Exited Game Over State");
    }
}


