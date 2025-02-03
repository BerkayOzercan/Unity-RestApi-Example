using System;

namespace Assets.GameSystem.Scripts
{
    public interface IGameState
    {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}


