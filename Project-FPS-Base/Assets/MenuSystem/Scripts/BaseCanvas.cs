using System;
using Assets.GameSystem.Scripts;
using UnityEngine;

namespace Assets.MenuSystem.Scripts
{
    public class BaseCanvas : MonoBehaviour
    {
        protected private GameManager _gameManager = null;
        protected private LevelManager _levelManager = null;
        protected private ScoreManager _scoreManager = null;

        protected virtual void Awake()
        {
            _gameManager = GameManager.Instance;
            _levelManager = LevelManager.Instance;
            _scoreManager = ScoreManager.Instance;
        }
    }
}


